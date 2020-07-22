//Controller class controls most of the game, including how pacman interacts with the ghosts and maze
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

namespace Pacman
{
    public class Controller
    {
        private const int STARTLIVES = 3;       //constants
        private const int PACSTARTX = 9;
        private const int PACSTARTY = 11;
        private const int GHOSTSTARTX = 10;
        private const int GHOSTSTARTY = 8;
        private const int DEATHPAUSE = 1000;
        private const int POSDIRECTIONS = 4;
        private const int BONUSPOINTS = 20;
        private const int LIVESBONUS = 100;
        private const int TOTALKIBBLE = 179;

        private Maze maze;
        private Pac pac;
        private List<Ghost> ghost;
        private bool open = true;
        private Random random;

        private int score = 0;
        private int lives = STARTLIVES;
        private int bonus = 0;

        public Controller(Maze maze, Random random) //controller creates a Pacman and 3 Ghosts
        {
            this.maze = maze;

            ghost = new List<Ghost>();

            this.random = random;

            pac = new Pac(Properties.Resources.pacman1right, maze);
            ghost.Add(new Ghost(Properties.Resources.red1, maze));
            ghost.Add(new Ghost(Properties.Resources.green1, maze));
            ghost.Add(new Ghost(Properties.Resources.purple1, maze));
        }

        public void StartGame()     //starts the game, runs on a timer tick to check if pacman has been caught
        {
            pac.Draw();
            foreach (Ghost g in ghost)
            {
                g.Draw();
                if (g.KillPacMan(pac.Position[0]) == true)
                {
                    lives--;
                    maze.DropPowerUp(pac.Position[0]);
                    SoundPlayer audiodeath = new SoundPlayer(Pacman.Properties.Resources.Death);
                    audiodeath.Play();
                    Respawn();
                }
            }
        }

        public void PacmanMove(Direction direction) //controls how pacman moves and detects
        {                                           //note: pacman only moves when the key is tapped, I made it this
            pac.Direction = direction;              //way because the game lags when he moves on the timer tick
            if (pac.HitWall() == false)             //bacause of this i needed a second method to check pacmans position
            {                                       //each time the key is pressed
                PacEat();
                pac.Move();
                SoundPlayer audio = new SoundPlayer(Pacman.Properties.Resources.wakka);
                audio.Play();
            }
            switch (direction)//animating pacman
            {
                case Direction.Left:
                    {
                        if(open == true)
                        {
                            pac.Body = Properties.Resources.pacman1left;
                        }
                        else
                        {
                            pac.Body = Properties.Resources.pacman2left;
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        if (open == true)
                        {
                            pac.Body = Properties.Resources.pacman1right;
                        }
                        else
                        {
                            pac.Body = Properties.Resources.pacman2right;
                        }
                        break;
                    }
                case Direction.Up:
                    {
                        if (open == true)
                        {
                            pac.Body = Properties.Resources.pacman1up;
                        }
                        else
                        {
                            pac.Body = Properties.Resources.pacman2up;
                        }
                        break;
                    }
                case Direction.Down:
                    {
                        if (open == true)
                        {
                            pac.Body = Properties.Resources.pacman1down;
                        }
                        else
                        {
                            pac.Body = Properties.Resources.pacman2down;
                        }
                        break;
                    }
            }
            open = !open;
            foreach (Ghost g in ghost)
            {
                if(pac.Die(g.Position[0]) == true)
                {
                    lives--;
                    maze.DropPowerUp(pac.Position[0]);
                    SoundPlayer audiodeath = new SoundPlayer(Pacman.Properties.Resources.Death);
                    audiodeath.Play();
                    Respawn();
                }
            }
        }

        public void PacEat() //allows pacman to eat and increments score
        {
            if (pac.Eat() == true)
            {
                score++;
                maze.TurnToBlank(pac.Position[0]);
            }
            if (pac.EatStar() == true)
            {
                bonus += BONUSPOINTS;
                maze.TurnToBlank(pac.Position[0]);
            }
        }

        public bool EndGame() //checks if the games win conditions are met 
        {            
            if (score == TOTALKIBBLE)
            {
                bonus += (LIVESBONUS * lives);
                return true;
            }
            return false;
        }

        public void Respawn() //method for respawning pacman and the ghosts
        {
            Thread.Sleep(DEATHPAUSE);
            pac.Position[0] = new Point (PACSTARTX, PACSTARTY);
            foreach (Ghost g in ghost)
            {
                g.Position[0] = new Point(GHOSTSTARTX, GHOSTSTARTY);
            }
        }

        public void GhostMove()   //controls how each ghost moves
        {
            foreach (Ghost g in ghost)
            {
                int chooseDirection = random.Next(POSDIRECTIONS);
                switch (chooseDirection)
                {
                    case 0:
                        g.Direction = Direction.Right;
                        if (g.Body == ghost[0].Body)
                        {
                            ghost[0].Body = Properties.Resources.red1;
                        }
                        if (g.Body == ghost[1].Body)
                        {
                            ghost[1].Body = Properties.Resources.green1;
                        }
                        if (g.Body == ghost[2].Body)
                        {
                            ghost[2].Body = Properties.Resources.purple1;
                        }
                        break;
                    case 1:
                        g.Direction = Direction.Left;
                        if (g.Body == ghost[0].Body)
                        {
                            ghost[0].Body = Properties.Resources.red2;
                        }
                        if (g.Body == ghost[1].Body)
                        {
                            ghost[1].Body = Properties.Resources.green2;
                        }
                        if (g.Body == ghost[2].Body)
                        {
                            ghost[2].Body = Properties.Resources.purple2;
                        }
                        break;
                    case 2:
                        g.Direction = Direction.Up;
                        break;
                    case 3:
                        g.Direction = Direction.Down;
                        break;
                    default:
                        break;
                }

                if (g.HitWall() == false)
                {
                    g.Move();
                }
            }
        }

        public int Score { get => score; set => score = value; }    //properties from this class
        public int Lives { get => lives; set => lives = value; }
        public int Bonus { get => bonus; set => bonus = value; }
    }
}
