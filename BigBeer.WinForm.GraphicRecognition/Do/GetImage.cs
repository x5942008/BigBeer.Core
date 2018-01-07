using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace BigBeer.WinForm.GraphicRecognition.Do
{
    public static class GetImage
    {
        /// <summary>
        /// 获取Image图片格式
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(FileStream file, out string format)
        {
            byte[] sb = new byte[2];  //这次读取的就是直接0-1的位置长度了.
            file.Read(sb, 0, sb.Length);
            //根据文件头判断
            string strFlag = sb[0].ToString() + sb[1].ToString();
            //察看格式类型
            switch (strFlag)
            {
                //JPG格式
                case "255216":
                    format = ".jpg";
                    return ImageFormat.Jpeg;
                //GIF格式
                case "7173":
                    format = ".gif";
                    return ImageFormat.Gif;
                //BMP格式
                case "6677":
                    format = ".bmp";
                    return ImageFormat.Bmp;
                //PNG格式
                case "13780":
                    format = ".png";
                    return ImageFormat.Png;
                //其他格式
                default:
                    format = string.Empty;
                    return null;
            }
        }

        /// <summary>
        /// 获取Image图片格式
        /// </summary>
        /// <param name="_img"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(Image _img, out string format)
        {
            if (_img.RawFormat.Equals(ImageFormat.Jpeg))
            {
                format = ".jpg";
                return ImageFormat.Jpeg;
            }
            if (_img.RawFormat.Equals(ImageFormat.Gif))
            {
                format = ".gif";
                return ImageFormat.Gif;
            }
            if (_img.RawFormat.Equals(ImageFormat.Png))
            {
                format = ".png";
                return ImageFormat.Png;
            }
            if (_img.RawFormat.Equals(ImageFormat.Bmp))
            {
                format = ".bmp";
                return ImageFormat.Bmp;
            }
            format = string.Empty;
            return null;
        }
        /// <summary>
        /// 网络图片转换IMAGE格式
        /// </summary>
        /// <param name="urlpath"></param>
        /// <returns></returns>
        public static Image Page_Load(string urlpath)
        {
            WebRequest myrequest = WebRequest.Create(urlpath);
            WebResponse myresponse = myrequest.GetResponse();
            Stream imgstream = myresponse.GetResponseStream();
            Image img = Image.FromStream(imgstream);
            //img.Save(Server.MapPath("test.jpg"),System.Drawing.Imaging.ImageFormat.Jpeg);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            return img;
        }
    }
}
