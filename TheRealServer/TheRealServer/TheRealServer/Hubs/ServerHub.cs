using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheRealServer.Models;

namespace TheRealServer.Hubs
{
    public class ServerHub : Hub
    {
        private readonly IHubContext<ServerHub> _hubContext;

        public ServerHub(IHubContext<ServerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMovement(PlayerPosition playerPosition)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMovement", playerPosition);
        }

        public async Task SendSpawnPoint(List<PlayerPosition> playerPositions)
        {
            await Clients.All.SendAsync("ReceiveSpawnPoint", playerPositions[0], playerPositions[1], playerPositions[2]);
        }
    }
}
