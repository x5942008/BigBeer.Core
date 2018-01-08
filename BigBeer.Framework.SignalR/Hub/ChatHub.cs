using Microsoft.AspNet.SignalR;

namespace BigBeer.Framework.SignalR
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// 自定义发送方法
        /// ALL后面可以自定义，调用根据自定义的函数名调用
        /// 整个项目只需要引用一个SignalR类库以及两个SignalR.JS即可
        /// </summary>
        /// <param name="name">接收的参数</param>
        /// <param name="message">接受的信息参数</param>
        public void Send(string name,string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
        public void Send(string message)
        {
            Clients.All.addNewMessageToPage(message);
        }
    }
}