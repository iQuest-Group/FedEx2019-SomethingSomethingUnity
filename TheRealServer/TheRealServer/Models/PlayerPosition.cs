using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRealServer.Models
{
    public class PlayerPosition
    {
        public PlayerPosition()
        {

        }
        public PlayerPosition(int Id, string Name, int PosX, int PosY)
        {
            this.Id = Id;
            this.PosX = PosX;
            this.PosY = PosY;
            this.Name = Name;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }
    }
}
