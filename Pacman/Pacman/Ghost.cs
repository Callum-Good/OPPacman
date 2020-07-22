using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Ghost : Character
    {
        private const int REDSTARTX = 10;
        private const int REDSTARTY = 8;

        public Ghost(Bitmap body, Maze maze)
            :base(body, maze)
        {
            this.body = body;

            position = new List<Point>();

            position.Add(new Point(REDSTARTX, REDSTARTY));
        }

        public bool KillPacMan(Point pacmanPos) //checks if ghost position = pacman's position, if so return true to kill pacman
        {
            if(position[0] == pacmanPos)
            {
                return true;
            }

            return false;
        }
    }
}
