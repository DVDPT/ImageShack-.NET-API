using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImagesHackDotNet.HttpExtensions
{
    public class FileMultipartParameter : IMultipartParameter
    {
        private readonly string _file;
        private readonly string _name;
        private readonly string _contentType;
        private readonly Encoding _encoding;

        private const string FileParameterTemplate =
            "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

        public FileMultipartParameter(string file, string name, string contentType,Encoding encoding)
        {
            _file = file;
            _name = name;
            _contentType = contentType;
            _encoding = encoding;
        }

        public FileMultipartParameter(string file, string name, string contentType) : this(file,name,contentType,Encoding.UTF8)
        {

        }

        public void Write(Stream s)
        {
            using (var file = new FileStream(_file, FileMode.Open, FileAccess.Read))
            {
                var header = String.Format(FileParameterTemplate, _name, _file, _contentType);
                s.Write(header, Encoding.UTF8);
                byte[] buffer = new byte[4096];
                int bytesRead = 0;

                while ((bytesRead = file.Read(buffer, 0, buffer.Length)) != 0)
                {
                    s.Write(buffer, 0, bytesRead);
                }

            }
        }
    }
}
