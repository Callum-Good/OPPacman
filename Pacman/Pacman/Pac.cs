//child class of Character.cs, contains aspects unique to pacman
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public class Pac : Character
    {
        private const int PACSTARTX = 9;
        private const int PACSTARTY = 11;
        
        public Pac(Bitmap body, Maze maze)
            :base(body, maze)
        {
            this.body = body;

            position = new List<Point>();

            position.Add(new Point(PACSTARTX, PACSTARTY));
        }

        public bool Eat()   //allows pacman to eat by matching his position with a kibbles or star
        {
            DataGridViewCell position = null;

            position = GridPos(new Point(Position[0].X, Position[0].Y));

            if(position.Value == maze.Kibble)
            {
                return true;
            }

            return false;
        }
        public bool EatStar()   //allows pacman to eat by matching his position with a kibbles or star
        {
            DataGridViewCell position = null;

            position = GridPos(new Point(Position[0].X, Position[0].Y));

            if (position.Value == maze.Star)
            {
                return true;
            }

            return false;
        }

        public bool Die(Point ghostPos)//checks for if pacs position = the ghosts position so pacman can die when he's moving
        {
            if(position[0] == ghostPos)
            {
                return true;
            }
            return false;
        }
    }
}
