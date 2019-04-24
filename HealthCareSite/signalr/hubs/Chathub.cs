using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LiveChat.signalr.hubs
{
    public class ChatHub : Hub
    {
        public void send(string name, string Message , string pic)
        {
            Clients.All.addNewMessageToPage(name, Message, pic  );

        }
    }
}