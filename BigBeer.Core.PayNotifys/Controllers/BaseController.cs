using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.PayNotifys.Controllers
{
    public class BaseController:Controller
    {
        //在此处注入需要使用的数据库

        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="content"></param>
        public async Task Log(string content)
        {
            try
            {
                FileStream fs = new FileStream(StaticKey.LogPath, FileMode.Append);
                StreamWriter w = new StreamWriter(fs);
                w.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}{content}");
                w.Flush();
                w.Close();
                fs.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///主要记录回调处理日志
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected Task log(string msg)
        {
            var path = $@"{ Directory.GetCurrentDirectory()}\log\{DateTime.Now.ToString("yyMMdd")}.txt";
            try
            {
                using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(msg);
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
