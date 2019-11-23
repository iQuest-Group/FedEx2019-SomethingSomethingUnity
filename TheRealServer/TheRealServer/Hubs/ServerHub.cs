using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheRealServer.Models;
using TheRealServer.Services;

namespace TheRealServer.Hubs
{
    public class ServerHub : Hub
    {
        private readonly IHubContext<ServerHub> _hubContext;
        private readonly ISpawnService spawnService;
        public ServerHub(ISpawnService spawnService, IHubContext<ServerHub> hubContext)
        {
            _hubContext = hubContext;
            this.spawnService = spawnService;
        }

        public async Task SendMovement(PlayerPosition playerPosition)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMovement", playerPosition);
        }

        public async Task SendSpawnPoint(List<PlayerPosition> playerPositions)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveSpawnPoints", playerPositions);
        }

        public async Task SendSingleSpawnPoint()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveSingleSpawnPoint", spawnService.GetPlayerSpawnPoint());
        }

        public async Task SendGameReset()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveGameReset");
        }

        public async Task SendPlayerLeave(int id)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveGameLeave", id);
        }
    }
}
