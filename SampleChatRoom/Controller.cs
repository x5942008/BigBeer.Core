using BeetleX.FastHttpApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleChatRoom
{
    [Controller]
    public class Controller
    {
        public bool Login(string nickName, IHttpContext context)
        {
            context.Session.Name = nickName;

            ActionResult result = new ActionResult();

            result.Data = new { name = nickName, message = "login", type = "login", time = DateTime.Now.ToString("T") };

            context.SendToWebSocket(result);
            return true;
        }
        //获取在线用户

        public object ListOnlines(IHttpContext context)

        {

            return from r in context.Server.GetWebSockets()

                   where r.Session.Name != null

                   select new { r.Session.Name, IP = r.Session.RemoteEndPoint.ToString() };

        }
        //发送聊天信息

        public bool Talk(string nickName, string message, IHttpContext context)

        {

            ActionResult result = new ActionResult();

            result.Data = new { name = nickName, message, type = "talk", time = DateTime.Now.ToString("T") };

            context.SendToWebSocket(result);

            return true;

        }
    }
}
