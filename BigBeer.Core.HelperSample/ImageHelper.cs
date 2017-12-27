using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Drawing2D;

namespace BigBeer.Core.HelperSample
{
    public static class ImageHelper
    {
        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="this"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Image Cut(this Image @this, int width, int height, int x, int y)
        {
            var r = new Bitmap(width, height);
            var destinationRectange = new Rectangle(0, 0, width, height);
            var sourceRectangle = new Rectangle(x, y, width, height);

            using (Graphics g = Graphics.FromImage(r))
            {
                g.DrawImage(@this, destinationRectange, sourceRectangle, GraphicsUnit.Pixel);
            }

            return r;
        }

        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="ratio">The ratio.</param>
        /// <returns>The scaled image to the specific ratio.</returns>
        public static Image Scale(this Image @this, double ratio)
        {
            int width = Convert.ToInt32(@this.Width * ratio);
            int height = Convert.ToInt32(@this.Height * ratio);

            var r = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(r))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(@this, 0, 0, width, height);
            }

            return r;
        }

        /// <summary>
        ///  缩放
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The scaled image to the specific width and height.</returns>
        public static Image Scale(this Image @this, int width, int height)
        {
            var r = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(r))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(@this, 0, 0, width, height);
            }

            return r;
        }

        /// <summary>
        /// 图片转换成base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ImgToBase64(this Image @this)
        {
            Image tempImg = @this;
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, tempImg);
            byte[] bytes = memStream.GetBuffer();
            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        /// <summary>
        /// UTF8转换byte
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] GetPayLoad(this string @this)
        {
            try
            {
                var temp = @this.ToJson();
                byte[] payload = new byte[temp.Length];
                payload = Encoding.UTF8.GetBytes(temp);
                return payload;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// base64转换成图片
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image Base64ToImg(this string @this)
        {
            var imageData = @this.Split(',');
            var buffer = Convert.FromBase64String(imageData[1]);
            var img = Image.FromStream(new MemoryStream(buffer));
            return img;
        }
    }
}
