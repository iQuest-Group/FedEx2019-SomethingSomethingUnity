using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class MessageHub : Hub
{
    public Task SendMessageToAll(string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", message);
    }

    public Task MoveObject(float dx, float dy)
    {
        return Clients.All.SendAsync("Move", dx, dy);
    }

}