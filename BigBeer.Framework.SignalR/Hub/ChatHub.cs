using Microsoft.AspNet.SignalR;

namespace BigBeer.Framework.SignalR
{
    public class ChatHub : Hub
    {
        public void Send(string name,string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}