using System;
using System.Drawing;
using System.IO;

namespace K94Warriors.Core.ImageResizing
{
    public class ImageResizer
    {
        public static byte[] ResizeToByteArray(byte[] originalImageBytes, int newWidth, int newHeight, 
            bool preserveAspectRatio = true)
        {
            using (var ms = new MemoryStream(originalImageBytes))
            {
                return Resize(ms, newWidth, newHeight, preserveAspectRatio);
            }
        }

        public static byte[] ResizeToByteArray(Stream originalImage, int newWidth, int newHeight,
            bool preserveAspectRatio = true)
        {
            return Resize(originalImage, newWidth, newHeight, preserveAspectRatio);
        }

        public static Stream ResizeToStream(byte[] originalImageBytes, int newWidth, int newHeight,
                                    bool preserveAspectRatio = true)
        {
            using (var ms = new MemoryStream(originalImageBytes))
            {
                return new MemoryStream(Resize(ms, newWidth, newHeight, preserveAspectRatio));
            }
        }

        public static Stream ResizeToStream(Stream originalImage, int newWidth, int newHeight,
                                            bool preserveAspectRatio = true)
        {
            return new MemoryStream(Resize(originalImage, newWidth, newHeight, preserveAspectRatio));
        }

        private static byte[] Resize(Stream originalImage, int newWidth, int newHeight,
                                             bool preserveAspectRatio = true)
        {
            var image = Image.FromStream(originalImage);
            var ratioX = (double)newWidth / image.Width;
            var ratioY = (double)newHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var width = (int)(image.Width * (preserveAspectRatio ? ratio : ratioX));
            var height = (int)(image.Height * (preserveAspectRatio ? ratio : ratioY));

            var newImage = new Bitmap(width, height);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);
            var bmp =  new Bitmap(newImage);

            var converter = new ImageConverter();
            return (byte[]) converter.ConvertTo(bmp, typeof (byte[]));
        }
    }
}