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
        public void OnMessage(string content)
        {
            try
            {
                FileStream fs = new FileStream(@"G:\wxpaymessage.txt", FileMode.Append);
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
    }
}
