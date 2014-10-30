using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_4
{
    public partial class Form1 : Form
    {
        private Brush[] color = new Brush[3];           //This will store 3 colors: White, Black, and Red
        private bool [,] takenSpot = new bool[8, 8];    //Store location of queens on a 8x8 array
        private int queens;                             //Keep track of number of Queens

        public Form1()
        {
            InitializeComponent();
        }

        //Check if move is valid: If it is, update the 2D bool array
        private bool validMove(int x, int y)
        {
            //Invalidate the row
            for (int i = 0; i < 8; i++)
            {
                if (takenSpot[i, y])
                {
                    return false;
                }
            }
            //Invalidate the column
            for (int j = 0; j < 8; j++)
            {
                if (takenSpot[x, j])
                {
                    return false;
                }
            }
            //Go through the diagonals and make them false (4 possible diagonal directions)
            int diag_1 = x;
            int diag_2 = x;
            int diag_3 = x;
            int diag_4 = x;
            //Invalidate diagonal spaces from right to left (going down)
            for (int k = y; ((diag_1 >= 0) && (k >= 0)) && ((diag_1 < 8) && (k < 8)); k--)
            {
                if (takenSpot[diag_1, k])
                {
                    return false;
                }
                diag_1--;
            }
            //Invalidate diagonal spaces from left to right (going down)
            for (int m = y; ((diag_2 >= 0) && (m >= 0)) && ((diag_2 < 8) && (m < 8)); m--)
            {
                if (takenSpot[diag_2, m])
                {
                    return false;
                }
                diag_2++;
            }
            //Invalidate diagonal spaces from right to left (going up)
            for (int n = y; ((diag_3 >= 0) && (n >= 0)) && ((diag_3 < 8) && (n < 8)); n++)
            {
                if (takenSpot[diag_3, n])
                {
                    return false;
                }
                diag_3--;
            }
            //Invalidate diagonal spaces from left to right (going up)
            for (int num10 = y; ((diag_4 >= 0) && (num10 >= 0)) && ((diag_4 < 8) && (num10 < 8)); num10++)
            {
                if (takenSpot[diag_4, num10])
                {
                    return false;
                }
                diag_4++;
            }
            //If no conditions are met to be FALSE, the space remains valid (TRUE)
            return true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //Create brush colors in the color array
            this.color[0] = Brushes.White;
            this.color[1] = Brushes.Black;
            this.color[2] = Brushes.Red;
            //Use this value to determine what brush to use
            int color_value;
            //Create the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //If hints box is checked and the box is false, color the square RED
                    if (this.hintBox.Checked && !validMove(i, j))
                    {
                        color_value = 2;
                    }
                    //If the sume of rows and column are even, color the square BLACK
                    else if (((i + j) % 2) == 0)
                    {
                        color_value = 0;
                    }
                    // else color the square WHITE
                    else
                    {
                        color_value = 1;                      
                    }
                    //Color the board
                    graphics.FillRectangle(color[color_value], 100 + (i * 50), 100 + (j * 50), 50, 50);
                    graphics.DrawRectangle(Pens.Black, 100 + (i * 50), 100 + (j * 50), 50, 50);
                    //Add the Queens
                    if (takenSpot[i, j])
                    {
                        //Upper left most point in the square
                        float x_corner = 100 + i * 50;
                        float y_corner = 100 + j * 50;
                        //Font for Queen 
                        Font drawFont = new Font("Arial", 30f, FontStyle.Bold);
                        //Draw rectangle as a blueprint for where to place the Q
                        RectangleF guide_box = new RectangleF(x_corner, y_corner, 50f, 50f);
                        //Need StringFormat parameter to place string in center of box
                        StringFormat centerLoc = new StringFormat();
                        centerLoc.Alignment = StringAlignment.Center;       //Horizontal middle
                        centerLoc.LineAlignment = StringAlignment.Center;   //Vertical Middle
                        //Use Drawstring that uses RectangleF for centered Queen, alternating color to show queen
                        graphics.DrawString("Q", drawFont, color[(color_value + 1) % 2], guide_box, centerLoc);
                    }
                    //This is the message on the top of the display
                    this.topMessage.Text = "You have " + queens + " queens on the board.";
                }
            }
        }

        //Checkbox for the Hints
        private void hintBox_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        //Clears the board
        private void cleanBoard_Click(object sender, EventArgs e)
        {
            takenSpot = new bool[8, 8];
            queens = 0;
            this.Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            //If a click is within the grid, check it
            if (((x >= 100) && (y >= 100)) && ((x <= 500) && (y <= 500)))
            {
                //Eliminate the offset (grid starts at 100,100)
                x = x - 100;
                y = y - 100;
                //Each box is 50 x 50,so by divding by 50, we get the row and column number
                x = x / 50;
                y = y / 50;
                //If a user left-clicks on a valid space, add a queen
                if (e.Button == MouseButtons.Left)
                {
                    //Check if space is valid
                    if (validMove(x, y))
                    {
                        takenSpot[x, y] = true;
                        ++queens;
                        //If there are Eight Queens, show popoup
                        if (queens == 8)
                        {
                            MessageBox.Show("Congratulations! You did it!");
                        }
                        this.Invalidate();
                    }
                    //Else play an error sound
                    else
                    {
                        SystemSounds.Beep.Play();
                    }
                }
                //If a user right-clicks on a queen, remove it
                else if ((e.Button == MouseButtons.Right) && takenSpot[x, y])
                {
                    --queens;
                    takenSpot[x, y] = false;
                    this.Invalidate();
                }
            }
        }
    }
}
