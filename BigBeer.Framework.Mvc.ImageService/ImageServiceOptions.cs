using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Framework.Mvc.ImageService
{
    public class ImageServiceOptions
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
        public Func<HttpResult, object> ResultFormart { get; set; } = (http) => {
            return new { s = http.Status, m = http.Message, d = http.Data };
        };
        /// <summary>
        /// 图片名称格式化
        /// </summary>
        public Func<string> ImageNameFormart { get; set; } = () => {
            return $"{DateTime.Now.Ticks}";
        };
    }
}
