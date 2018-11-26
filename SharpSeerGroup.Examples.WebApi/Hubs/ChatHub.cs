using Microsoft.AspNetCore.SignalR;
using SharpSeerGroup.Examples.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSeerGroup.Examples.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public static List<string> OnlineUsers { get; } = new List<string>();
        public MongoDB.Driver.IMongoCollection<ChatMessage> ChatCollection { get; set; }

        public ChatHub(Mongo db)
        {
            ChatCollection = db.GetMessagesCollection();
        }

        public override Task OnConnectedAsync()
        {
            Clients.Others.SendAsync("UserOnline", this.Context.User.Identity.Name);
            Clients.Caller.SendAsync("OnlineUsers", OnlineUsers);
            OnlineUsers.Add(Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.SendAsync("UserOffline", this.Context.User.Identity.Name);
            OnlineUsers.Remove(Context.User.Identity.Name);
            return base.OnDisconnectedAsync(exception);
        }

        public Task PrivateMessage(string user, string message)
        {
            var chatMessage = new ChatMessage()
            {
                From = Context.User.Identity.Name,
                To = user,
                Message = message
            };
            ChatCollection.InsertOne(chatMessage);
            return Clients.User(user).SendAsync("ReceivePrivateMessage", chatMessage);
        }

        public Task Broadcast(string message)
        {
            var chatMessage = new ChatMessage()
            {
                From = Context.User.Identity.Name,
                Message = message
            };
            ChatCollection.InsertOne(chatMessage);
            return Clients.All.SendAsync("ReceiveBroadcastMessage", chatMessage);
        }
    }
}
