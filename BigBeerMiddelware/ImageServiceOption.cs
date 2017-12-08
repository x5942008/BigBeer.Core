using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeerMiddelwareSample
{
    public class ImageServiceOption
    {
        /// <summary>
        /// 上传路径
        /// </summary>
        public string Uploadpath { get; set; } = "/image/upload";
        /// <summary>
        /// 显示路径
        /// </summary>
        public string Displaypath { get; set; } = "/image/display";
        /// <summary>
        /// 保存路径
        /// </summary>
        public string Savepath { get; set; } = @"F:\ImageServer";
        /// <summary>
        /// 结果格式化
        /// </summary>
        public Func<ImageResult, object> ResultFormart { get; set; } = (http) => {
            return new { s = http.Status, m = http.Message, d = http.Data };
        };
        /// <summary>
        /// 图片名称格式化
        /// </summary>
        public Func<string> ImageNameFormart { get; set; } = () => {
            return $"{DateTime.Now.Ticks}";
        };
       
    }

    public class ImageResult
    {
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; }
        public bool Status { get; set; } = true;

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ImageResult Success(string message = "ok", object data = null)
        {
            return new ImageResult() { Message = message, Data = data };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ImageResult Faild(string message = "error", object data = null)
        {
            return new ImageResult() { Message = message,Status =false, Data = data };
        }
    }
}
