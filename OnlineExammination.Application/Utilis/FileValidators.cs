using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Application.Utilis
{
    public static class FileValidators
    {
        #region Heplers
        public static void IsImageFormat(this IFormFile file)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".svg" };
            string extension = Path.GetExtension(file.FileName);


            if (!imageExtensions.Contains(extension))
            {
                throw new FormatException("Image format is invalid");
            }
        }



        public static void IsImageFormat(this IFormFileCollection files)
        {
            foreach (var file in files)
            {
                file.IsImageFormat();
            }
        }


        public static void IsVideoFormat(this IFormFile file)
        {
            string[] videoExtensions = { ".mp4", ".avi", ".mpeg", ".mpg", ".m4v" };

            string extension = Path.GetExtension(file.FileName);


            if (!videoExtensions.Contains(extension))
            {
                throw new FormatException("Video format is invalid");
            }
        }


        public static void IsVideoFormat(this IFormFileCollection files)
        {

            foreach (var file in files)
            {
                file.IsVideoFormat();
            }
        }


        public static void IsVideoOrImageFormat(this IFormFileCollection files)
        {

            files.IsImageFormat();
            files.IsVideoFormat();
        }

        public static void IsVideoOrImageFormat(this IFormFile file)
        {

            file.IsImageFormat();
            file.IsVideoFormat();
        }
        #endregion Heplers

    }
}
