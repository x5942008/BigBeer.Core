using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.WinForm.GraphicRecognition.Do
{
    public static class HighPrecision
    {
        /// <summary>
        /// 高精度识别
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Accurate(string path)
        {
            var image = File.ReadAllBytes(path);
            // 调用通用文字识别（高精度版），可能会抛出网络等异常，请使用try/catch捕获
            var result = AI.client.AccurateBasic(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
        {"detect_direction", "true"},
    };
            // 带参数调用通用文字识别（高精度版）
            result = AI.client.GeneralBasic(image, options);
            var temp = result["words_result"];
            if (temp == null)
            {
                return "该图无法识别";
            }
            var ai = new StringBuilder();
            for (int i = 0; i < temp.Count(); i++)
            {
                ai.Append(temp[i].ToString().Split(':')[1].Replace("\"", "").Replace("}", ""));
            }
            return ai.ToString();
        }
    }
}
