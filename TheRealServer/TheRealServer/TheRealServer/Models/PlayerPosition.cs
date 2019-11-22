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
        public PlayerPosition(int Id, int posX, int posY)
        {
            this.Id = Id;
            this.posX = posX;
            this.posY = posY;
        }
        public int Id { get; set; }

        public int posX { get; set; }

        public int posY { get; set; }
    }
}
