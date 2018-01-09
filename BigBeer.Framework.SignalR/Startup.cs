using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(BigBeer.Framework.SignalR.Startup))]

namespace BigBeer.Framework.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll);//允许跨域请求
            app.MapSignalR();
            //app.MapSignalR<TrackerConnection>("/tracker");//配置连接
        }
    }
    /// <summary>
    /// 持久连接
    /// </summary>
    public class TrackerConnection : PersistentConnection
    {
        private static int connections = 0;
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            Interlocked.Increment(ref connections);
            Debug.WriteLine("Visitors"+connections);
            
           // return this.Connection.Broadcast("通知的消息",new[] {connectionId,"123","456" });//不通知指定connectionId的用户
            return base.OnConnected(request, connectionId);
        }
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Increment(ref connections);
            Debug.WriteLine("Visitors" + connections);

            return base.OnDisconnected(request, connectionId, stopCalled);
        }
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var message = JsonConvert.DeserializeObject<ChatMessage>(data);
            if (message.messageType == MessageType.Private)
            {
                var text = message.Text;
                //...
            }
            //...
            return base.OnReceived(request, connectionId, data);
        }
    }
    /// <summary>
    /// 临时类
    /// </summary>
    internal class ChatMessage
    {
        internal string Text { get; set; }
        internal MessageType messageType { get; set; }
    }

    public enum MessageType
    {
        Private,
        Public,
        Internal,
        Protected
    }
}
