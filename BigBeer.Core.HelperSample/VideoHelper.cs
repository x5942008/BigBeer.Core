using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BigBeer.Core.HelperSample
{
    /// <summary>
    /// 视频处理
    /// </summary>
    public class VideoHelper
    {
        /// <summary>
        /// ffmpeg.exe路径
        /// </summary>
        public string Path { get; set; }

        public VideoHelper(string path)
        {
            Path = path;
        }

        /// <summary>
        /// 压缩视频
        /// </summary>
        /// <param name="sourceFlie">源文件地址</param>
        /// <param name="targetPath">结果存放路径</param>
        /// <param name="resolutionRatio">分辨率,例如1280x720.</param>
        public void ConvertVideo(string sourceFlie, string targetPath, string resolutionRatio)
        {
            string strArg = $"-i {sourceFlie} -y -s {resolutionRatio} {targetPath} ";
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = Path;
            p.StartInfo.Arguments = strArg;
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口

            p.ErrorDataReceived += (a, b) =>
            {
                if (!String.IsNullOrEmpty(b.Data))
                {
                    //处理方法...
                    Console.WriteLine(b.Data);
                }
            };//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN

            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }

        /// <summary>
        /// 执行自定义命令
        /// </summary>
        /// <param name="strArg">命令代码</param>
        public void ExecuteCommand(string strArg, Action<object, DataReceivedEventArgs> func)
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = Path;
            p.StartInfo.Arguments = strArg;
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.ErrorDataReceived += new DataReceivedEventHandler(func);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }
    }

    /// <summary>
    /// 使用范例
    /// </summary>
    public class VideoTest
    {
        public void DoSomeThing() { 
        string targetPath = @"ffmpeg\aaa.mp4";//目标存放路径
        string sourceFile = @"ffmpeg\test_mp4.mp4";//源文件路径
                                                   //编码
        string strArg = "-i " + sourceFile + " -y -s 640x480 " + targetPath + " ";
        //string strArg = "-i " + sourceFile + " -y -s 1280x720 " + playFile + " ";

        //截取图片jpg
        //string strArg = "-i " + sourceFile + " -y -f image2 -t 1 " + imgFile;
        //string strArg = "-i " + sourceFile + " -y -s 1280x720 -f image2 -t 1 " + imgFile;

        //视频截取
        //string strArg = "  -i " + sourceFile + " -y   -ss 0:20  -frames 100  " + playFile;

        //转化gif动画
        //string strArg = "-i " + sourceFile + " -y -s 1280x720 -f gif -vframes 30 " + imgFile;
        //string strArg = "  -i " + sourceFile + " -y  -f gif -vframes 50 " + imgFile;
        // string strArg = "  -i " + sourceFile + " -y  -f gif -ss 0:20  -dframes 10 -frames 50 " + imgFile;

        //显示基本信息
        //string strArg = "-i " + sourceFile + " -n OUTPUT";

        //播放视频 
        //string strArg = "-stats -i " + sourceFile + " ";

        VideoHelper videoHelper = new VideoHelper(@"ffmpeg\bin\ffmpeg.exe");
        videoHelper.ConvertVideo(sourceFile, targetPath,"640x480");
            string result = "长一些方便查看测试结果长一些方便查看测试结果长一些方便查看测试结果长一些方便查看测试结果长一些方便查看测试结果长一些方便查看测试结果长一些方便查看测试结果长一些方便查看测试结果";
        videoHelper.ExecuteCommand(strArg, (a, b) =>
            {
                Console.WriteLine(b.Data);
            });

            Console.WriteLine(result);
            Console.Read();
        }
    }
}
