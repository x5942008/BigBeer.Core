using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.HelperSample
{
    public class HttpHelper
    {
        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url">地址,后面一节</param>
        /// <param name="paramses">参数</param>
        /// <param name="method">方法</param>
        /// <param name="headers">headers</param>
        /// <returns></returns>
        private static string Request(string url, string paramses = null, Dictionary<string, string> headers = null, string method = "post", string contentType = null, string encod = null)
        {
            Encoding encoding;
            if (!string.IsNullOrEmpty(encod))
            {
                encoding = Encoding.GetEncoding(encod);
            }
            else
            {
                encoding = Encoding.UTF8;
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            method = method.ToLower();
            request.Method = method;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;
            }
            switch (method)
            {
                case "get":
                    break;
                default:
                    request.ContentLength = 0;
                    if (paramses != null)
                    {
                        byte[] bs = encoding.GetBytes(paramses);
                        request.ContentLength = bs.Length;
                        using (Stream reqStream = request.GetRequestStream())
                        {
                            reqStream.Write(bs, 0, bs.Length);
                        }
                    }
                    break;
            }

            using (var stream = request.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, encoding);
                string result = reader.ReadToEnd();
                reader.Close();
                return result;

            }
        }

        public static string Post(string url, string paramses = null, Dictionary<string, string> headers = null, string contentType = "application/json", string encod = null)
        {
            try
            {
                var result = Request(url, paramses, headers, "post", contentType, encod);
                return result;
            }
            catch (Exception)
            {
                return "false";
            }
        }

        public static Task<string> PostAsync(string url, string paramses = null, Dictionary<string, string> headers = null, string contentType = "application/json", string encod = null)
        {
            return Task.Run(() =>
            {
                var result = Post(url, paramses, headers, contentType, encod);
                return result;
            });
        }

        public static string Get(string url, string encod = null)
        {
            var result = Request(url, method: "get", encod: encod);
            return result;
        }

        public static Task<string> GetAsync(string url, string encod = null)
        {
            return Task.Run(() =>
            {
                var result = Get(url, encod);
                return result;
            });
        }


        /// <summary>
        /// ApiCloud平台推送消息
        /// </summary>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="groupName">groupName - 推送组名，多个组用英文逗号隔开.默认:全部组</param>
        /// <param name="userIds">推送用户id, 多个用户用英文逗号分隔</param>
        /// <param name="type">消息类型，1:消息 2:通知</param>
        /// <param name="platform">platform - 0:全部平台，1：ios, 2：android</param>
        /// <returns></returns>
        public static async Task<string> AppPushAsync(string title, string content, string groupName = null, string userIds = null, int type = 1, int platform = 0)
        {
            string result = "";
            try
            {
                //string id = ConfigurationManager.AppSettings["ApiCloudAppId"];
                //string key = ConfigurationManager.AppSettings["ApiCloudAppKey"];
                string id = "A6942863313950";
                string key = "E046DF86-7BDB-DBFB-0459-2B5CD5EDF25A";
                Dictionary<string, string> paramses = new Dictionary<string, string>
                {
                    { "title", title },
                    { "content", content },
                    { "type", type.ToString() },
                    { "platform", platform.ToString() }
                };
                if (!String.IsNullOrEmpty(groupName)/*.IsNullOrEmpty()*/)
                {
                    paramses.Add("groupName", groupName);
                }
                if (!String.IsNullOrEmpty(userIds)/*.IsNullOrEmpty()*/)
                {
                    paramses.Add("userIds", userIds);
                }
                var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                var temp = $"{id}UZ{key}UZ{epoch}";
                var appKey = $"{SHA1(temp, Encoding.UTF8).ToLower()}.{epoch}";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("X-APICloud-AppId", id);
                headers.Add("X-APICloud-AppKey", appKey);
                result = await PostAsync("https://p.apicloud.com/api/push/message", paramses.ToJsonPost(), headers, "application/x-www-form-urlencoded");
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
    }

    /// <summary>
    /// 范例
    /// </summary>
    public class HttpTest
    {
        public void DoSomeThing()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "role","21"}
            };
            var result = HttpHelper.Post("http://10.0.0.5/Home/Index", dictionary.ToJsonPost(), contentType: "application/x-www-form-urlencoded");
        }

    }
}
