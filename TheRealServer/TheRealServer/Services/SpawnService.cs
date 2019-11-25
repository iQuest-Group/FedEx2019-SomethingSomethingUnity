using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRealServer.Models;

namespace TheRealServer.Services
{
    public class SpawnService : ISpawnService
    {
        private Dictionary<PlayerPosition, bool> spawnPoints;
        private int order = 0;
        public SpawnService()
        {
            spawnPoints = new Dictionary<PlayerPosition, bool>();
            spawnPoints.Add(new PlayerPosition(1, "ALEX1", 2, 4), false);
            spawnPoints.Add(new PlayerPosition(2, "ALEX2", 3, 1), false);
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
            spawn.Order = order++;
            return spawn;
        }

        public void ResetGame()
        {
            spawnPoints = spawnPoints.ToDictionary(w => w.Key, w => w.Key != null ? false : true);
            foreach(var sp in spawnPoints)
            {
                sp.Key.Order = 0;
            }
        }

        public void LeaveGame(int id)
        {
            var player = spawnPoints.FirstOrDefault(p => p.Key.Id == id);
            if(player.Key != null)
            {
                spawnPoints[player.Key] = false;
                player.Key.Order = 0;
            }
        }

        public List<PlayerPosition> GetPlayerPositions()
        {
            GetPlayerSpawnPoint();
            var sp = spawnPoints.Where(x => x.Value == true).Select(y => y.Key);
            return sp.ToList();
        }
    }
}
