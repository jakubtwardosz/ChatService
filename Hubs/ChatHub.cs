using ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs;

public class ChatHub : Hub
{
    public async Task JoinChat(UserConnection userConnection)
    {
        await Clients.All
            .SendAsync("ReceiveMessage", "admin", $"{userConnection.Username} has joined");
    }

    public async Task JoinSpecificChatRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);
        await Clients.Group(userConnection.ChatRoom)
            .SendAsync("JoinSpecificChatRoom", "admin", $"{userConnection.Username} has joined {userConnection.ChatRoom}");
    }
}