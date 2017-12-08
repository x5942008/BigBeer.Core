using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeerMiddelwareSample
{
    public class ImageUploadMiddleware
    {
        public RequestDelegate Next;
        public ImageServiceOption Service;
        public ImageUploadMiddleware(RequestDelegate next, ImageServiceOption service)
        {
            Next = next;
            Service = service;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.ContentType.ToLower() != "post")
            {
                var result = Service.ResultFormart(ImageResult.Faild("不允许的方式"));
                return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
            context.Response.ContentType = "application/json";
            var image = ReadImg(context);
            return Next(context);
        }
        /// <summary>
        /// 保存图片,返回url
        /// </summary>
        /// <param name="images"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        private string[] SaveImages(string[] images, string host)
        {
            var paths = "";
            var result = new List<string>();
            foreach (var img in images)
            {
                var filename = $"{paths}.jpg";
                var path = Path.Combine(paths, filename);

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
        /// base64转为byte
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
        string[] ReadImg(HttpContext context)
        {
            try
            {
                var forms = Forms(context);
                return JsonConvert.DeserializeObject<string[]>(forms["images"]);
            }
            catch (Exception)
            {
                return new string[] { };
            }
        }
        /// <summary>
        /// 获取提交的数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Dictionary<string, string> Forms(HttpContext context)
        {
            var formsString = new StreamReader(context.Request.Body).ReadToEnd();
            Console.WriteLine(formsString);
            if (formsString.StartsWith("{"))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(formsString);
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var p in formsString.Split('&'))
            {
                var p1 = p.Split(new char[] { '=' });
                if (!p1.Any()) continue;
                if (p1.Count() < 2) continue;
                result.Add(p1[0], System.Net.WebUtility.UrlDecode(p1[1]));
            }
            return result;
        }
    }
}
