/* Program name: 	    Pacman
   Project file name:   Pacman
   Author:		        Callum Good
   Date:	            26/10/18
   Language:		    C#
   Platform:		    Microsoft Visual Studio 2017
   Purpose:		        This is the classic arcade game Pacman, where the player controlled character "Pacman" is chased
                        around a maze while being chased by ghosts. Pacman must eat all the kibble pieces in the maze before being caught
                        by the ghosts.
   Description:		    The player controls Pacman with the arrow keys and navigates a maze filled with kibble. Pacman eats the kibble 
                        and makes it disappear by passing over it. There are also AI Ghosts in the maze that kill Pacman when they touch
                        him. If caught, the player loses a life. The game ends when the lives = 0 or pacman has eaten all the kibble in 
                        the maze.
   Known Bugs:		    None.
   Additional Features: Sounds, extra points for picking up "stars" that are dropped when Pacman dies, extra points for finishing the
                        game with lives to spare. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Form1 : Form
    {
        private const int FORMHEIGHT = 583;
        private const int FORMWIDTH = 900;

        //declare the Maze object so it can be used throughout the form
        private Maze maze;
        private Controller controller;
        private Random random;

        public Form1()                  //Form1 constructor. Initialising the maze and controller
        {
            InitializeComponent();

            // set the Properties of the form:
            Top = 0;
            Left = 0;
            Height = FORMHEIGHT;
            Width = FORMWIDTH;
            KeyPreview = true;

            // create a Bitmap object for each image you want to display
            Bitmap k = Properties.Resources.kibble;
            Bitmap w = Properties.Resources.wall2;
            Bitmap b = Properties.Resources.blank;
            Bitmap s = Properties.Resources.star;

            // create an instance of a Maze:
            maze = new Maze(k, w, b, s);
            maze.CellBorderStyle = DataGridViewCellBorderStyle.None;

            random = new Random();
            controller = new Controller(maze, random);

            // important, need to add the maze object to the list of controls on the form
            Controls.Add(maze);

            // remember the Timer Enabled Property is set to false as a default
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)    //events that happen on a timer tick, or methods that constantly need checked
        {
            if (controller.EndGame() == true)
            {
                SoundPlayer audio = new SoundPlayer(Pacman.Properties.Resources.win);
                audio.Play();
                updatetext();
                timer1.Enabled = false;
                winMessage();
            }
            if (controller.Lives < 0)   //intentionally checking for when lives are < 0. this is so the label displays "0" lives left i.e.
            {                           //if you die with 0 lives left, you lose
                SoundPlayer audio = new SoundPlayer(Pacman.Properties.Resources.EndGame);
                audio.Play();
                timer1.Enabled = false;
                loseMessage();
            }

            controller.GhostMove();
            controller.PacEat();
            updatetext();
            maze.Draw();
            controller.StartGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   //set up controls
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    controller.PacmanMove(Direction.Left);
                    break;

                case Keys.Right:
                    controller.PacmanMove(Direction.Right);
                    break;

                case Keys.Up:
                    controller.PacmanMove(Direction.Up);
                    break;

                case Keys.Down:
                    controller.PacmanMove(Direction.Down);
                    break;

                case Keys.P:
                    pause();
                    break;

                case Keys.Q:
                    Application.Exit();
                    break;

                default:
                    break;
            }
        }

        private void updatetext()   //update text boxes for various events
        {
            label2.Text = Convert.ToString(controller.Score);
            label6.Text = Convert.ToString(controller.Bonus);
            if (controller.Lives == 3)
            {
                label4.ForeColor = System.Drawing.Color.LimeGreen;
            }
            if (controller.Lives == 2)
            {
                label4.ForeColor = System.Drawing.Color.Yellow;
            }
            if (controller.Lives == 1)
            {
                label4.ForeColor = System.Drawing.Color.Orange;
            }
            if (controller.Lives == 0)
            {
                label4.ForeColor = System.Drawing.Color.Red;
            }
            label4.Text = Convert.ToString(controller.Lives);
            label8.Text = Convert.ToString(controller.Score + controller.Bonus);
        }

        private void button1_Click(object sender, EventArgs e)  //pause button in form1
        {
            pause();
        }
        private void pause()                    //method for pausing
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)  //quit button in form
        {
            Application.Exit();
        }

        private void winMessage()           //win message / play again
        {
            DialogResult result = MessageBox.Show("Do you want to play again?", "Congradulations, you win", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SoundPlayer audio = new SoundPlayer(Pacman.Properties.Resources.intro);
                audio.Play();
                this.Hide();
                Form1 f1 = new Form1(); //credit stackoverflow
                f1.ShowDialog();
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void loseMessage()      //lose message / play again
        {
            DialogResult result = MessageBox.Show("Do you want to play again?", "YOU LOSE", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SoundPlayer audio = new SoundPlayer(Pacman.Properties.Resources.intro);
                audio.Play();
                this.Hide();
                Form1 f1 = new Form1(); //credit stackoverflow
                f1.ShowDialog();
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }
    }
}
