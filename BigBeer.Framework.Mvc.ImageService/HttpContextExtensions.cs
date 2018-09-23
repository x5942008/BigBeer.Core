using BigBeer.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.ImageService
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task Log(this HttpContext httpContext, string message, string title = null, bool hasTime = true)
        {
            var directory = $@"{ Directory.GetCurrentDirectory()}\log\image\";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var path = $@"{directory}{DateTime.Now.ToString("yyMMdd")}.txt";
            try
            {
                using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        if (hasTime)
                            writer.WriteLine(DateTime.Now.ToString());
                        if (title.IsNotNullOrEmpty())
                            writer.WriteLine(title);
                        writer.WriteLine(message);
                        writer.Flush();
                        writer.Close();
                    }
                    stream.Flush();
                    stream.Close();
                }
            }
            catch
            {

            }
            return Task.CompletedTask;
        }
    }
}
