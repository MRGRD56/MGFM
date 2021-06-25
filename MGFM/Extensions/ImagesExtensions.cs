using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MGFM.Extensions
{
    public static class ImagesExtensions
    {
        /// <summary>
        /// Returns a <see cref="MemoryStream"/> for the <paramref name="bitmap"/>.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static MemoryStream GetStream(this Bitmap bitmap, ImageFormat imageFormat)
        {
            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, imageFormat);
            return memoryStream;
        }

        /// <summary>
        /// Returns a <see cref="MemoryStream"/> for a <see cref="Bitmap"/> with PNG format.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static MemoryStream GetStream(this Bitmap bitmap) => bitmap.GetStream(ImageFormat.Png);

        /// <summary>
        /// Returns bytes of the <paramref name="bitmap"/>.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static Span<byte> GetBytes(this Bitmap bitmap, ImageFormat imageFormat)
        {
            using var memoryStream = bitmap.GetStream();
            var bytes = memoryStream.ToArray();
            return bytes;
        }

        /// <summary>
        /// Returns bytes of the <paramref name="bitmap"/> with PNG format.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Span<byte> GetBytes(this Bitmap bitmap) => bitmap.GetBytes(ImageFormat.Png);

        /// <summary>
        /// Converts the <paramref name="bitmap"/> to an <see cref="ImageSource"/>.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageSource ToImageSource(this Bitmap bitmap)
        {
            using var bitmapStream = bitmap.GetStream();

            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            imageSource.CacheOption = BitmapCacheOption.OnLoad;
            imageSource.StreamSource = bitmapStream;
            imageSource.EndInit();
            return imageSource;
        }
    }
}
