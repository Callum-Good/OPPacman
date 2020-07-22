//parent class for characters in the game. 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public abstract class Character
    {
        protected Bitmap body;
        protected Maze maze;
        protected List<Point> position;
        private Direction direction;

        public Character(Bitmap body, Maze maze)
        {
            this.body = body;
            this.maze = maze;
            direction = Direction.Right;
        }

        public void Draw()//each character must draw itself
        {
            maze.Rows[position[0].Y].Cells[position[0].X].Value = body;
        }         

        public DataGridViewCell GridPos(Point p)//lets characters now their position on the grid
        {
            return maze.Rows[p.Y].Cells[p.X];
        }

        public void Move()  //controls how the characters move along the grid
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        position[0] = new Point(position[0].X - 1, position[0].Y);
                        break;
                    }
                case Direction.Right:
                    {
                        position[0] = new Point(position[0].X + 1, position[0].Y);
                        break;
                    }
                case Direction.Up:
                    {
                        position[0] = new Point(position[0].X, position[0].Y - 1);
                        break;
                    }
                case Direction.Down:
                    {
                        position[0] = new Point(position[0].X, position[0].Y + 1);
                        break;
                    }
            }
        }
        public bool HitWall()   //method for wall detection
        {
            DataGridViewCell position = null;

            switch (direction)
            {
                case Direction.Up:
                    position = GridPos(new Point(Position[0].X, Position[0].Y - 1));
                    break;
                case Direction.Down:
                    position = GridPos(new Point(Position[0].X, Position[0].Y + 1));
                    break;
                case Direction.Left:
                    position = GridPos(new Point(Position[0].X - 1, Position[0].Y));
                    break;
                case Direction.Right:
                    position = GridPos(new Point(Position[0].X + 1, Position[0].Y));
                    break;
                default:
                    break;
            }

            if(position.Value == maze.Wall)
            {
                return true;
            }
            return false;
        }

        public Direction Direction { get => direction; set => direction = value; }//properties for this class
        public Bitmap Body { get => body; set => body = value; }
        public List<Point> Position { get => position; set => position = value; }
    }
}
