using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImagesHackDotNet.HttpExtensions
{
    public static class Extensions
    {
        public static void Write(this Stream s, string data, Encoding encoding)
        {
            var arr = encoding.GetBytes(data);
            s.Write(arr, 0, arr.Length);
        }

        public static string GetContentType(this FileInfo info)
        {
            switch (info.Extension.ToLower())
            {
                case ".jpg":
                    return "image/jpg";
                    
                case ".jpeg":
                    return"image/jpeg";
                    
                case ".gif":
                    return"image/gif";
                    
                case ".png":
                    return"image/png";
                    
                case ".bmp":
                    return"image/bmp";
                    
                case ".tif":
                    return"image/tiff";
                    
                case ".tiff":
                    return"image/tiff";
                    
                default:
                    return"image/unknown";
                    

            }
        }
    }
}
