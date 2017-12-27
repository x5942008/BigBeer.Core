using BigBeer.Core.HelperSample;
using System;
using System.Collections.Generic;

namespace BigBeer.Core.WeChat.Author
{
    public class WebAuthor
    {
        public WebAuthor() { }

        /// <summary>
        /// 自动实例化获取授权
        /// </summary>
        /// <param name="code"></param>
        public WebAuthor(string code)
        {
            try
            {
                string url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={Config.AppId}&secret={Config.Secret}&code={code}&grant_type={Config.GrantType}";
                var PostString = HttpHelper.Post(url, contentType: null);
                var result = PostString.ToObjectJson<Dictionary<string, string>>();
                AccessToken = result["access_token"];
                RefreshToken = result["refresh_token"];
                OpenId = result["openid"];
            }
            catch (Exception)
            {

            }
        }

        public Config Config { get; set; } = new Config();

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string OpenId { get; set; }

        public Dictionary<string, string> UserInfo()
        {
            try
            {
                string url = "https://api.weixin.qq.com/sns/userinfo?access_token={AccessToken}&openid={OpenId}";
                var PostString = HttpHelper.Post(url, contentType: null);
                var result = PostString.ToObjectJson<Dictionary<string, string>>();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
