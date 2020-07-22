//the maze class sets up the maze
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{ 
    public class Maze : DataGridView
    {
        private const int NROWSCOLUMNS = 20;                          // Number of cells in each row and column
        private const int CELLSIZE = 27;
        private const int SPACESIZE = 4;

                                    //12345678901234567890
    private const string INITMAP =  "wwwwwwwwwwwwwwwwwwww" +//1
                                    "wkkkkkkkkkkkkkkkkkkw" +//2
                                    "wkwwkwkwkwwkwkwkwwkw" +//3
                                    "wkwwkwkwkkkkwkwkwwkw" +//4
                                    "wkkkkwkwkwwkwkwkkkkw" +//5
                                    "wkwwwwkwkwwkwkwwwwkw" +//6
                                    "wkkkkkkkkkkkkkkkkkkw" +//7
                                    "wkwwwwkwwbbwwkwwkwkw" +//8
                                    "wkwwkkkwwbbwwkwwkwkw" +//9
                                    "wkkkkwkwwbbwwkwkkwkw" +//10
                                    "wkwwwwkwwwwwwkwkwwkw" +//11
                                    "wkkkkkkkkbkkkkkkkkkw" +//12
                                    "wkwwwwkwwkwwkwwwwwkw" +//13
                                    "wkwwwwkwwkwwkwwwwwkw" +//14
                                    "wkwwwwkkkkkkkkkkkkkw" +//15
                                    "wkkkkkkwkwkwwwkwwwkw" +//16
                                    "wkwwwwkwkwkwwwkwwwkw" +//17
                                    "wkwwwwkwkwkwwwkwwwkw" +//18
                                    "wkkkkkkkkkkkkkkkkkkw" +//19
                                    "wwwwwwwwwwwwwwwwwwww";//20                         

    //fields
        private string map;
        private int nKibbles;
        private Bitmap wall;
        private Bitmap kibble;
        private Bitmap blank;
        private Bitmap star;
            
        //constructor
        public Maze(Bitmap k, Bitmap w, Bitmap b, Bitmap s)
            :base()
        {
            //initialise fields
            map = INITMAP;
            wall = w;
            kibble = k;
            blank = b;
            star = s;     
            // set position of maze on the Form
            Top = 0;
            Left = 0;
                
            // setup the columns to display images. We want to display images, so we set 5 columns worth of Image columns
            for (int x = 0; x < NROWSCOLUMNS; x++)
            {
                Columns.Add(new DataGridViewImageColumn());  
            }
            // then we can tell the grid the number of rows we want to display
            RowCount = NROWSCOLUMNS;

            // set the properties of the Maze(which is a DataGridView object)
            Height = NROWSCOLUMNS * CELLSIZE + SPACESIZE;
            Width = NROWSCOLUMNS * CELLSIZE + SPACESIZE; 
            ScrollBars = ScrollBars.None;
            ColumnHeadersVisible = false;
            RowHeadersVisible = false; 
                
            // set size of cells:
            foreach (DataGridViewRow r in this.Rows)
                r.Height = CELLSIZE;

            foreach (DataGridViewColumn c in this.Columns)
                c.Width = CELLSIZE;

            // rows and columns should never resize themselves to fit cell contents
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // prevent user from resizing rows or columns
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
        }

        

    //to draw the maze, the string character is used to load the corresponding image into the DataGridView cell
    public void Draw()
    {
            int totalCells = NROWSCOLUMNS * NROWSCOLUMNS;
                
            for (int i = 0; i < totalCells; i++)
            {
                    int nRow = i / NROWSCOLUMNS;
                    int nColumn = i % NROWSCOLUMNS;

                        switch (map.Substring(i,1))
                        {
                            case "w":                                 
                                Rows[nRow].Cells[nColumn].Value = wall;
                                break;
                            case "k":              
                                Rows[nRow].Cells[nColumn].Value = kibble;
                                break;
                            case "b":
                                Rows[nRow].Cells[nColumn].Value = blank;
                                break;
                            case "s":
                                Rows[nRow].Cells[nColumn].Value = star;     //added star
                                break;
                            default:
                                MessageBox.Show("Unidentified value in string");
                                break;
                        }
            }
    }
        
    public void TurnToBlank(Point position) //turns the cell blank once pacman has "eaten" the kibble
    {
        if(Rows[position.Y].Cells[position.X].Value == kibble || Rows[position.Y].Cells[position.X].Value == star)
        {
                StringBuilder edit = new StringBuilder(map);
                edit[((position.Y * NROWSCOLUMNS) + position.X)] = 'b';
                map = edit.ToString();
        }
    }
    
    public void DropPowerUp(Point position) //drops a star on pacmans position 
    {
            StringBuilder edit = new StringBuilder(map);
            edit[((position.Y * NROWSCOLUMNS) + position.X)] = 's';
            map = edit.ToString();
    }

    public Bitmap Wall  //properties for this class
    {
        get { return wall; }
        set { wall = value; }
    }

    public Bitmap Kibble
    {
        get { return kibble; }
        set { kibble = value; }
    }
    
    public Bitmap Star
    {
        get { return star; }
        set { kibble = value; }
    }

    }

}
