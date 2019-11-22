﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TheRealServer.Hubs;
using TheRealServer.Models;

namespace TheRealServer.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        List<PlayerPosition> playerSpawnPoint;
        ServerHub serverHub;
        public ServerController(ServerHub hub)
        {
            playerSpawnPoint = new List<PlayerPosition>();
            playerSpawnPoint.Add(new PlayerPosition(1, 5, 7));
            playerSpawnPoint.Add(new PlayerPosition(1, 7, 9));
            playerSpawnPoint.Add(new PlayerPosition(1, 1, 2));
            serverHub = hub;
        }

        [HttpGet]
        [Route("spawn")]
        public IEnumerable<PlayerPosition> GetSpawnPoint()
        {
            return playerSpawnPoint;
        }

        [HttpPost]
        [Route("move")]
        public async Task PostMovement([FromBody] PlayerPosition playerPosition)
        {
            await serverHub.SendMovement(playerPosition);
        }

        [HttpPost]
        [Route("attack")]
        public async Task PostAttack([FromBody] string something)
        {

        }

    }
}
