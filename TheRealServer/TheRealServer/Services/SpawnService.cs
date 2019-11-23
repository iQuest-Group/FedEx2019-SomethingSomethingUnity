using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRealServer.Models;

namespace TheRealServer.Services
{
    public class SpawnService : ISpawnService
    {
        Dictionary<PlayerPosition, bool> spawnPoints;
        public SpawnService()
        {
            spawnPoints = new Dictionary<PlayerPosition, bool>();
            spawnPoints.Add(new PlayerPosition(1, "ALEX1", 5, 7), false);
            spawnPoints.Add(new PlayerPosition(2, "ALEX2", 7, 9), false);
            spawnPoints.Add(new PlayerPosition(3, "ALEX3", 1, 2), false);
        }

        public PlayerPosition GetPlayerSpawnPoint()
        {
            if(spawnPoints.Values.All(value => value == true))
            {
                return null;
            }

            Random rand = new Random();
            List<PlayerPosition> values = Enumerable.ToList(spawnPoints.Keys);
            int size = spawnPoints.Count;

            PlayerPosition spawn;
            do
            {
                spawn = values[rand.Next(size)];
            } while (spawnPoints.GetValueOrDefault(spawn));

            spawnPoints[spawn] = true;

            return spawn;
        }
    }
}
