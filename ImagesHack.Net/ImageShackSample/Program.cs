using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagesHackDotNet;

namespace ImageShackSample
{
    class Program
    {
        public static void Main(string[] args)
        {
            ImageShackApiWrapper.DeveloperKey = "your_key_here";
            var imageInfo = ImageShackApiWrapper.Upload(@"D:\Downloads\Chorme\a-game-of-thrones-book-cover.bmp");

            Console.WriteLine(imageInfo.Links.ImageLink);
        }
    }
}
