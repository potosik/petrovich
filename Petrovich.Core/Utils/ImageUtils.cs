using Petrovich.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Utils
{
    public static class ImageUtils
    {
        public static byte[] GetFullImageByteArray(Image image)
        {
            return GetByteArray(image, Constants.Base64Images.BigWidth, Constants.Base64Images.BigHeight);
        }

        private static byte[] GetByteArray(Image image, int width, int height)
        {
            if (image == null)
            {
                return null;
            }

            var sizedImage = ResizeImage(image, width, height);
            return ToByteArray(sizedImage);
        }

        private static byte[] ToByteArray(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static string GetDefaultImageString(Image image)
        {
            return GetImageString(image, Constants.Base64Images.DefaultWidth, Constants.Base64Images.DefaultHeight);
        }

        public static string GetSmallImageString(Image image)
        {
            return GetImageString(image, Constants.Base64Images.SmallWidth, Constants.Base64Images.SmallHeight);
        }

        public static string GetImageString(Image image, int width, int height)
        {
            return GetByteArray(image, width, height).ToBase64String();
        }
    }
}
