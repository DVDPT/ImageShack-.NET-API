using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImagesHackDotNet.HttpExtensions
{
    public class BasicMultipartParameter : IMultipartParameter
    {
        private readonly string _name;
        private readonly string _value;
        private readonly Encoding _encoding;

        const string SimpleParameterTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

        public BasicMultipartParameter(string name, string value, Encoding encoding)
        {
            _name = name;
            _value = value;
            _encoding = encoding;
        }

        public BasicMultipartParameter(string name, string value) : this(name,value,Encoding.UTF8)
        {
            
        }

        public void Write(Stream s)
        {
            var parameter = String.Format(SimpleParameterTemplate, _name, _value);
            s.Write(parameter, _encoding);
        }
    }
}
