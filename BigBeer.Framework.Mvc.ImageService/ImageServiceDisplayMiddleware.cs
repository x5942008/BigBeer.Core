using BigBeer.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.ImageService
{
    /// <summary>
    /// 图片显示服务
    /// </summary>
    public class ImageServiceDisplayMiddleware
    {
        public RequestDelegate Next { get; set; }
        public ImageServiceOptions serverOptions { get; set; }
        public ImageServiceDisplayMiddleware(RequestDelegate next, ImageServiceOptions options)
        {
            Next = next;
            serverOptions = options;
        }
        public Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            if (context.Request.Method.ToLower() != "get")
            {
                var result = serverOptions.ResultFormart(HttpResult.Faild("不允许的方法"));
                return context.Response.WriteAsync(result.ToJson());
            }
            var filename = context.Request.Path.Value.Replace("/", string.Empty);
            var path = Path.Combine(serverOptions.Savepath, filename);
            try
            {
                var size = Query(context.Request);

                var buffer = ReadImage(filename, path, size.w, size.h);
                return context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                return context.Log(ex.Message, "图片读取错误!");
            }


        }

        byte[] ReadImage(string name, string path, int? w = null, int? h = null)
        {
            var buffer = new byte[] { };
            if (w.HasValue || h.HasValue)
            {
                var cache = Path.Combine(serverOptions.Savepath, $"{w ?? 0}x{h ?? 0}{name}");
                if (File.Exists(cache))
                {
                    using (var stream = new FileStream(cache, FileMode.Open, FileAccess.Read))
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        stream.Flush();
                        stream.Close();
                    }
                    return buffer;
                }
            }
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (w.HasValue || h.HasValue)
                    return ZoomImage(name, stream, w, h);
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
            }

            return buffer;
        }

        byte[] ZoomImage(string name, Stream stream, int? width, int? height)
        {
            var img = Image.FromStream(stream);
            var ms = new MemoryStream();
            int w = width.HasValue ? width.Value : (int)(img.Width * (height.Value / (double)img.Height));
            int h = height.HasValue ? height.Value : (int)(img.Height * (width.Value / (double)img.Width));
            var path = Path.Combine(serverOptions.Savepath, $"{width ?? 0}x{height ?? 0}{name}");
            try
            {
                img.GetThumbnailImage(w, h, new Image.GetThumbnailImageAbort(() => false), (IntPtr)Int32.MaxValue)
                                .Save(ms, ImageFormat.Jpeg);
                img.Dispose();
                var buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Flush();
                ms.Close();
                ms.Dispose();
                Save(buffer, path);
                return buffer;
            }
            catch (Exception ex)
            {

                return new byte[] { };
            }



        }

        (int? w, int? h) Query(HttpRequest request)
        {
            if (!request.QueryString.HasValue)
                return (null, null);
            if (request.Query.ContainsKey("p"))
            {
                var arry = request.Query["p"].ToString().Split('x');
                if (!arry.Any() || arry.Count() < 2)
                    return (null, null);
                try
                {
                    return (int.Parse(arry[0]), int.Parse(arry[1]));
                }
                catch (Exception)
                {
                    return (null, null);
                }
            }
            if (request.Query.ContainsKey("w") || request.Query.ContainsKey("h"))
            {
                int? w = null, h = null;
                if (int.TryParse(request.Query["w"].ToString(), out var ww))
                    w = ww;

                if (int.TryParse(request.Query["h"].ToString(), out var hh))
                    h = hh;

                return (w, h);
            }
            return (null, null);
        }
        void Save(byte[] stream, string path)
        {
            try
            {
                using (var filestream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    filestream.Write(stream, 0, stream.Length);
                    filestream.Flush();
                    filestream.Dispose();
                }
            }
            catch
            {

            }

        }
    }
}
