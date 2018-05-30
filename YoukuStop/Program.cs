using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


/* 
 * 优酷桌面无法退出解决方案 自用
 * 通过控制台程序关闭后台进程
 * 目前Core生成EXE比较麻烦 所以直接使用framework控制台来实现
 * App.Config 里可以修改关闭的程序名称 方便后期修改和扩展
 */
namespace YoukuStop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start".GetJson());
            Log("测试方法");

            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcesses();
            foreach (var item in ps)
            {
                if (item.ProcessName.Contains(Tools.Process))
                {
                    Console.WriteLine($"关闭进程:{item.Id}:{item.ProcessName}");
                    StopProcess(item.ProcessName);
                }

            }

            Console.WriteLine("End".GetJson());

            Console.ReadLine();
        }

        #region 清除进程
        /// <summary>
        /// 清除进程
        /// </summary>
        /// <param name="processName"></param>
        private void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程   
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    //找到程序进程,kill之。
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }

            }
            catch (Exception Ex)
            {
                Log(Ex.Message);
                Console.WriteLine(Ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        public static void StopProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(processName);
                foreach (System.Diagnostics.Process p in ps)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        private static void Log(string message)
        {
            var path = $@"C:\Users\BigBeer\Desktop";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path =$@"{path}\log.txt";
            using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var writer = new StreamWriter(stream))
                {

                    writer.WriteLine(DateTime.Now.ToString());
                    writer.WriteLine(message);
                    writer.Flush();
                    writer.Close();
                }
                //stream.Flush();
                stream.Close();
            }
        }
    }
}
