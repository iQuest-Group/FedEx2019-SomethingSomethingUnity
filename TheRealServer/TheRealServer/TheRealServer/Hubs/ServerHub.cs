using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TheRealServer.Hubs
{
    public class ServerHub : Hub
    {
        public async Task SendMovement(float posX = 19, float posY = 20)
        {
            await Clients.All.SendAsync("ReceiveMovement", posX, posY);
        }
    }
}
