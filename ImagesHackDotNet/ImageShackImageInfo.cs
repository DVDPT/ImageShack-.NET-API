using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ImageShackDotNet
{
    public class ImageShackImageInfo
    {
        public class RatingDetails
        {
            public int Ratings { get; internal set; }
            public double Average { get; internal set; }

        }

        public class ResolutionDetails
        {
            public int Width { get; internal set; }
            public int Height { get; internal set; }


        }

        public class LinkDetails
        {
            public string ImageLink { get; internal set; }

            public string ThumbLink { get; internal set; }

            public string YfrogLink { get; internal set; }
        }


        public RatingDetails Rating { get; private set; }
        public ResolutionDetails Resolution { get; private set; }
        public LinkDetails Links { get; private set; }

        public ImageShackImageInfo(Stream imageInfoXml)
        {
            Rating = new RatingDetails();
            Resolution = new ResolutionDetails();
            Links = new LinkDetails();
            ParseAndFillProperties(imageInfoXml);
        }

        private void ParseAndFillProperties(Stream imageInfoXml)
        {
            var reader = XmlReader.Create(imageInfoXml);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "ratings":
                            Rating.Ratings = reader.ReadElementContentAsInt();
                            break;
                        case "avg":
                            Rating.Average = reader.ReadElementContentAsDouble();
                            break;
                        case "image_link":
                            Links.ImageLink = reader.ReadString();
                            break;
                        case "thumb_link":
                            Links.ThumbLink = reader.ReadString();
                            break;
                        case "yfrog_link":
                            Links.YfrogLink = reader.ReadString();
                            break;


                        case "width":
                            Resolution.Width = reader.ReadElementContentAsInt();
                            break;


                        case "height":
                            Resolution.Height = reader.ReadElementContentAsInt();
                            break;


                        default:
                            break;
                    }
                }
            }
        }
    }
}
