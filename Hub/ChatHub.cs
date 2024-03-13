namespace ChatAppBackend.Hub;
using Microsoft.AspNetCore.SignalR; 

public class ChatHub : Hub
{
    public async Task JoinChat(UserRoomConnection userRoomConnection)
    {
        await Clients.All.SendAsync("JoinChat", "admin", $"{userRoomConnection.User} has joined");
    }

public async Task JoinSpecificChatRoom(UserRoomConnection userRoomConnection)
{
    if (userRoomConnection.RoomName == null)
    {
        // Handle the case where room name is null
        return;
    }

    await Groups.AddToGroupAsync(Context.ConnectionId, userRoomConnection.RoomName);
    await Clients.Group(userRoomConnection.RoomName)
        .SendAsync("ReciveMessage", "admin", $"{userRoomConnection.User} has joined", userRoomConnection.RoomName);
}

}




    // public class ChatHub : Hub
    // {
    //     private readonly IDictionary<string, UserRoomConnection> _connection;
    //     public ChatHub(IDictionary<string, UserRoomConnection> connection)
    //     {
    //         _connection = connection;
    //     }
    //     public async Task JoinRoom(UserRoomConnection userRoomConnection)
    //     {
    //         await Groups.AddToGroupAsync(Context.ConnectionId, userRoomConnection.RoomName!);
            
    //         // Add the user to the room connection
    //         _connection[Context.ConnectionId] = userRoomConnection;
    //         await Clients.Group(userRoomConnection.RoomName!)
    //         .SendAsync("ReceiveMessage","admin", $"{userRoomConnection.User} has joined the room {userRoomConnection.RoomName}");
    //         await SendConnectionUser(userRoomConnection.RoomName!);
    //     }

    //     public async Task JoinSpecificRoom(UserRoomConnection userRoomConnection)
    //     {
    //         await Groups.AddToGroupAsync(Context.ConnectionId, userRoomConnection.RoomName!);
    //         await Clients.Group(userRoomConnection.RoomName!)
    //         .SendAsync("JoinSpecificRoom", userRoomConnection.User, userRoomConnection.RoomName!);
    //     }

    //     public async Task SendMessage(string message)
    //     {
    //         if (_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection? userRoomConnection))
    //         {
    //             await Clients.Group(userRoomConnection.RoomName!)
    //             .SendAsync("ReceiveMessage", userRoomConnection.User, message, DateTime.Now);
    //         }

    //     }

    //     public override Task OnDisconnectedAsync(Exception? exception)
    //     {
    //         if (!_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection? userRoomConnection))
    //         {
    //             return base.OnDisconnectedAsync(exception);
    //         }
    //         Clients.Group(userRoomConnection.RoomName!)
    //         .SendAsync("ReceiveMessage", "Let's Program bot", $"{userRoomConnection.User} has left the room {userRoomConnection.RoomName}");
    //         SendConnectionUser(userRoomConnection.RoomName!);

    //         return base.OnDisconnectedAsync(exception);
    //     }

    //     public Task SendConnectionUser(string roomName)
    //     {
    //         var users = _connection.Values.Where(x => x.RoomName == roomName).Select(x => x.User);
    //         return Clients.Group(roomName).SendAsync("ConnectedUser", users);
    //     }
    // }
