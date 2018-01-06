using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.WinForm.GraphicRecognition.Do
{
    public class GeneralBasic
    {
        // 设置APPID/AK/SK
        //static string APP_ID = "你的 App ID";
        static string API_KEY = "vGgwbTEi6iEASwUh5uw8Eq25";
        static string SECRET_KEY = "uZeTaYeZZ5GoxxWihYWVVUau5scHEH0l";
        public static Baidu.Aip.Ocr.Ocr client => new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);

        public string General(string path)
        {
            var image = File.ReadAllBytes(path);
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasic(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
        //{"language_type", "CHN_ENG"},
        {"detect_direction", "true"},
        //{"detect_language", "true"},
        //{"probability", "true"}
    };
            // 带参数调用通用文字识别, 图片参数为本地图片
            result = client.GeneralBasic(image, options);
            List<string> list = new List<string>();
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
        public string GeneralBasicUrl(string urlpath)
        {
            //var url = "https//www.x.com/sample.jpg";

            // 调用通用文字识别, 图片参数为远程url图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasicUrl(urlpath);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
        //{"language_type", "CHN_ENG"},
        {"detect_direction", "true"},
        //{"detect_language", "true"},
        //{"probability", "true"}
    };
            // 带参数调用通用文字识别, 图片参数为远程url图片
            result = client.GeneralBasicUrl(urlpath, options);
            List<string> list = new List<string>();
            var temp = result["words_result"];
            var ai = new StringBuilder();
            for (int i = 0; i < temp.Count(); i++)
            {
                ai.Append(temp[i].ToString().Split(':')[1].Replace("\"", "").Replace("}", ""));
            }
            return ai.ToString();
        }
    }
}
