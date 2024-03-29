﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TheRealServer.Hubs;
using TheRealServer.Models;
using TheRealServer.Services;

namespace TheRealServer.Controllers
{
	[Route("api/game")]
	[ApiController]
	public class ServerController : ControllerBase
    {
		ServerHub serverHub;
        ISpawnService spawnService;

        public ServerController(ISpawnService spawnService, ServerHub hub)
		{
            this.spawnService = spawnService;
			serverHub = hub;
		}

		[HttpGet]
		[Route("spawn")]
		public async Task GetSpawnPoint()
		{
            await serverHub.SendSpawnPoint(spawnService.GetPlayerPositions());
		}

        [HttpPost]
        [Route("reset")]
        public async Task ResetGame()
        {
            spawnService.ResetGame();
            await serverHub.SendGameReset();
        }

        [HttpPost]
        [Route("leave")]
        public async Task LeaveGame([FromBody] int id)
        {
            spawnService.LeaveGame(id);
            await serverHub.SendPlayerLeave(id);
        }

		[HttpPost]
		[Route("move")]
		public async Task PostMovement([FromBody] PlayerPosition playerPosition)
		{
			await serverHub.SendMovement(playerPosition);
		}

		//[HttpPost]
		//[Route("attack")]
		//public async Task PostAttack([FromBody] string something)
		//{

		//}

	}
}
