using Petrovich.Core.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Extensions
{
    public static class ImageExtensions
    {
        public static string GetFullImageString(this Image image)
        {
            return ImageUtils.GetFullImageString(image);
        }

        public static string GetDefaultImageString(this Image image)
        {
            return ImageUtils.GetDefaultImageString(image);
        }

        public static string GetSmallImageString(this Image image)
        {
            return ImageUtils.GetSmallImageString(image);
        }
    }
}
