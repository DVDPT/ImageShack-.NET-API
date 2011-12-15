using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageShackDotNet;
using ImagesHackDotNet.HttpExtensions;

namespace ImagesHackDotNet
{
    public class ImageShackApiWrapper
    {
        public static string DeveloperKey { get; set; }

        public static ImageShackImageInfo Upload(string file)
        {
            var fileInfo = new FileInfo(file);
            if(!fileInfo.Exists)
                throw new FileNotFoundException(file);

            if(string.IsNullOrEmpty(DeveloperKey))
                throw new InvalidOperationException("Can't call ImageShack API without a Developer Key");

            var sender = new HttpPostMultipartFormDataSender("http://www.imageshack.us/upload_api.php");
            sender.WithData("key", DeveloperKey);
            sender.WithFileData("fileupload", file, fileInfo.GetContentType());
            var response = sender.GetResponse();

            try
            {
                return new ImageShackImageInfo(response.GetResponseStream());
            }
            finally
            {
                response.Close();
            }
        }
    }
}
