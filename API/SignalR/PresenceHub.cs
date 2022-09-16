using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker __tracker;
        public PresenceHub(PresenceTracker _tracker)
        {
            __tracker = _tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var isOnline = await __tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId);
            if (isOnline)
                await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername()); // Send to all the connected clients apart from the caller 

            var currentUsers = await __tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers); // Update to only those who has connected
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isOffline = await __tracker.UserDisconnected(Context.User.GetUsername(), Context.ConnectionId);
            if(isOffline)
                await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername()); // Notify other connected users that user is offline

            await base.OnDisconnectedAsync(exception);
        }
    }
}