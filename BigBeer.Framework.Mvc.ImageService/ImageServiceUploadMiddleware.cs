using BigBeer.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.ImageService
{
    /// <summary>
    /// 图片上传服务
    /// </summary>
    public class ImageServiceUploadMiddleware
    {
        public RequestDelegate Next { get; set; }
        public ImageServiceOptions serverOptions { get; set; }
        public ImageServiceUploadMiddleware(RequestDelegate next, ImageServiceOptions options)
        {
            Next = next;
            serverOptions = options;
        }
        public Task Invoke(HttpContext context)
        {
            if (context.Request.Method.ToLower() != "post")
            {
                var result = serverOptions.ResultFormart(HttpResult.Faild("不允许的方法"));
                return context.Response.WriteAsync(result.ToJson());

            }

            context.Response.ContentType = "application/json";

            if (!context.User.Identity.IsAuthenticated)
            {
                var result = serverOptions.ResultFormart(HttpResult.Faild("未授权用户"));
                return context.Response.WriteAsync(result.ToJson());

            }

            var images = ReadImage(context);
            if (!images.Any())
            {
                var result = serverOptions.ResultFormart(HttpResult.Faild("参数错误,请使用images 参数"));
                return context.Response.WriteAsync(result.ToJson());

            }
            var imagelist = SaveImages(images, $"{context.Request.Scheme}://{context.Request.Host}");

            var obj = serverOptions.ResultFormart(HttpResult.Success("ok", imagelist));
            return context.Response.WriteAsync(obj.ToJson());
        }
        /// <summary>
        /// 保存图片,返回url
        /// </summary>
        /// <param name="images"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        private string[] SaveImages(string[] images, string host)
        {
            var result = new List<string>();
            foreach (var img in images)
            {
                var filename = $"{serverOptions.ImageNameFormart()}.jpg";
                var path = Path.Combine(serverOptions.Savepath, filename);

                var url = $"{host}{serverOptions.Displaypath.Replace('\\', '/')}/{filename}";
                using (var stream = new FileStream(path, FileMode.CreateNew))
                {
                    var buffer = FromBase64String(img);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                    stream.Close();
                }
                result.Add(url);
            }
            return result.ToArray();
        }
        /// <summary>
        /// base64转换为byte
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        byte[] FromBase64String(string base64String)
        {
            var base64 = base64String;
            if (base64String.StartsWith("data:image"))
                base64 = base64String.Split(new char[] { ',' })[1];
            return Convert.FromBase64String(base64);
        }
        /// <summary>
        /// 读取base64字符串
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string[] ReadImage(HttpContext context)
        {

            try
            {
                var forms = Forms(context);
                return forms["images"].ToObject<string[]>();
            }
            catch (Exception)
            {
                return new string[] { };
            }
        }
        /// <summary>
        /// 获取提交的数据
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public Dictionary<string, string> Forms(HttpContext @this)
        {
            var formsString = new System.IO.StreamReader(@this.Request.Body).ReadToEnd();
            Console.WriteLine(formsString);
            if (formsString.StartsWith("{"))
                return formsString.ToObject<Dictionary<string, string>>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var p in formsString.Split('&'))
            {
                var pl = p.Split(new char[] { '=' });
                if (!pl.Any()) continue;
                if (pl.Count() < 2) continue;
                result.Add(pl[0], System.Net.WebUtility.UrlDecode(pl[1]));
            }
            return result;
        }

    }
}
