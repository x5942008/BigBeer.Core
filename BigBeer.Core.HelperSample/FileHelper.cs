using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.HelperSample
{
    public static class FileHelper
    {
        /// <summary>
        /// 从text文档中随机获取一个值
        /// </summary>
        /// <param name="path">text文件完整路径</param>
        /// <param name="split">text文档中分隔符</param>
        /// <returns></returns>
        public static async Task<string> RandomValue(string path, char split)
        {
            var stream = new StreamReader(path, Encoding.UTF8);
            string str = await stream.ReadToEndAsync();
            string[] arr = str.Split(split);

            Random random = new Random();
            var index = random.Next(0, arr.Length - 1);

            return arr[index];
        }

        /// <summary>
        /// 保存TXT
        /// </summary>
        /// <param name="path">text文件完整路径</param>
        /// <param name="split">text文档中分隔符</param>
        /// <returns></returns>
        public static async Task<string> SaveFile(string path, char split)
        {
            var stream = new StreamReader(path, Encoding.UTF8);
            string str = await stream.ReadToEndAsync();
            string[] arr = str.Split(split);

            Random random = new Random();
            var index = random.Next(0, arr.Length - 1);

            return arr[index];
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="this">日志内容</param>
        /// <param name="logPath">保存路径</param>
        /// <param name="i">重试次数(默认即可)</param>
        public static void SaveLog(string @this, string logPath, int i = 0)
        {
            try
            {
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                string filePath = $"{logPath}{DateTime.Now.ToString("yyyy/MM/dd/ HH:mm:ss")}.txt";
                byte[] myByte = Encoding.UTF8.GetBytes(@this + "\n");
                using (StreamWriter write = new StreamWriter(@filePath, true))
                {
                    write.WriteLine(DateTime.Now.ToString());
                    write.WriteLine(@this);
                    write.Flush();

                };
            }
            catch (Exception)
            {
                if (i < 10)
                    Task.Run(async () =>
                    {
                        await Task.Delay(200);
                        SaveLog(@this, logPath, i++);
                    });
            }
        }
    }
}
