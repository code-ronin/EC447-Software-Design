using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private ArrayList coordinates = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Point p = new Point(e.X, e.Y);
                PointValues p = new PointValues(e.X, e.Y, false);
                this.coordinates.Add(p);
                this.Invalidate();
            }
            if (e.Button == MouseButtons.Right)
            {
                //PointValues p = (PointValues)coordinates[index];
                bool in_circle = false;
                //Loop through ArrayList to check where mouse right-clicked
                //Use a forloop backwards-if you delete something, it won't generate out of bounds error
                int total = coordinates.Count;
                int index = total - 1;
                for (int i = index; i >= 0; i--)
                {
                    PointValues p = (PointValues)coordinates[i];
                    //Check if it clicked within circle areas
                    if ((Math.Abs(e.X - p.X) < 10) && (Math.Abs(e.Y - p.Y) < 10))
                    {
                        //If BLACK, make RED
                        if (p.red_black == false)
                        {
                            p.red_black = true;
                            in_circle = true;
                            this.Invalidate();
                        }
                        //IF RED, delete it
                        else if (p.red_black == true)
                        {
                            coordinates.RemoveAt(i);
                            in_circle = true;
                            this.Invalidate();
                        }
                    }
                }
                //If no circles were right clicked, remove eveyrthing
                if (in_circle == false)
                {
                    this.coordinates.Clear();
                    this.Invalidate();
                }

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            const int WIDTH = 20;
            const int HEIGHT = 20;
            Graphics g = e.Graphics;
            foreach (PointValues p in this.coordinates)
            {
                if (p.red_black)
                {
                    g.FillEllipse(Brushes.Red, p.X- WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT);
                }
                if (!p.red_black)
                {
                    //String coordinates = string.Format("({0},{1})", p.x_coor, p.y_coor);
                    g.FillEllipse(Brushes.Black, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT);
                    //g.DrawString(coordinates, Font, Brushes.Black, p.x_coor + 15, p.y_coor - 5);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.coordinates.Clear();
            this.Invalidate();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.coordinates.Clear();
            this.Invalidate();
        }
    }


    public class PointValues
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool red_black { get; set; } //TRUE = RED, FALSE = BLACK
        public PointValues(int x_coor, int y_coor, bool color)
        {
            X = x_coor;
            Y = y_coor;
            red_black = color;
        }
    }
}
