using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRealServer.Models;

namespace TheRealServer.Services
{
    public interface ISpawnService
    {
        PlayerPosition GetPlayerSpawnPoint();

        void ResetGame();

        void LeaveGame(int id);

        List<PlayerPosition> GetPlayerPositions();
    }
}
