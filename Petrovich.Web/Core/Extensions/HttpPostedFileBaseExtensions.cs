using Petrovich.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Core.Extensions
{
    public static class HttpPostedFileBaseExtensions
    {
        public static Image GetImage(this HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }

            try
            {
                return Image.FromStream(file.InputStream);
            }
            catch
            {
                throw new InvalidImageFormatException(file.FileName);
            }
        }
    }
}