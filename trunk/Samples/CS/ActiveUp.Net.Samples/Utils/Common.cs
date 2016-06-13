using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveUp.Net.Samples.Utils
{
    class Common
    {
        /// <summary>
        /// Get the image path of the application
        /// </summary>
        /// <returns>the image path</returns>
        public static string GetImagePath(string fullPath)
        {
            string imagePath = fullPath;
            string imageDir = "";
            if (imagePath[0] == 34)
                if (imagePath.Length > 2)
                    imagePath = imagePath.Substring(1, imagePath.Length - 2);
            int index = -1;
            for (int i = imagePath.Length - 1; i >= 0; i--)
            {
                if (imagePath[i] == 92)
                {
                    index = i;
                    break;
                }

            }

            if (index != -1)
            {
                imageDir = imagePath.Substring(0, index);
            }

            return imageDir;
        }
    }
}
