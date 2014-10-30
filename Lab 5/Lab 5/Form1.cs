using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_5
{
    public partial class Form1 : Form
    {
        private ArrayList objects = new ArrayList();    //Store graphic objects drawn in this array
        private bool mouseFirst = true;                 //For keeping track of the first click
        private Point startPoint;
        private Point endPoint;
        public Form1()
        {
            InitializeComponent();
            //Set initial index to start at the top of the columns
            this.PenColor.SelectedIndex = 0;
            this.FillColor.SelectedIndex = 0;
            this.PenWidth.SelectedIndex = 0;
        }
        //Clear the objects drawn on the draw_panel
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.objects.Clear();
            this.drawPanel.Invalidate();
            base.Update();
        }
        //Undo least object created
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.objects.Count > 0) //Check to make sure count is greater than 0
            {
                this.objects.RemoveAt(this.objects.Count - 1);
            }
            this.drawPanel.Invalidate();
            base.Update();
        }
        //Exit the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        //The drawPanel is where everything is drawn
            //drawPanel paint handler
        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //When any of the objects are selected (line, rectangle, ellipse, text) draw it
            foreach (MyObject myObject in this.objects)
            {
                myObject.draw(graphics);
            }
        }
            //drawPanel mouse handler
        private void drawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //Document the first click in the drawPanel
            if (this.mouseFirst)
            {
                this.startPoint = e.Location;
                this.mouseFirst = false;
                return;
            }
            //Reset the mouseFirst upon the second click
            this.endPoint = e.Location;
            this.mouseFirst = true;
            Brush brush = null;     //Pen Color
            Brush brush2 = null;    //Fill Color
            Pen pen = null;         //Pen
            //Set the Pen Color
            switch (this.PenColor.SelectedIndex)
            {
                case 0:
                    brush = Brushes.Black;
                    break;
                case 1:
                    brush = Brushes.Red;
                    break;
                case 2:
                    brush = Brushes.Blue;
                    break;
                case 3:
                    brush = Brushes.Green;
                    break;
            }
            //If either Line object or Outline box is checked, you need to make the pen the correct color and width
            if (this.outline_box.Checked || this.lineButton.Checked)
            {
                pen = new Pen(brush, (float)int.Parse((string)this.PenWidth.SelectedItem));
            }
            //Set Fill Color, only if filBox is checked
            if (this.fill_box.Checked)
            {
                switch (this.FillColor.SelectedIndex)
                {
                    case 0:
                        brush2 = Brushes.White;
                        break;
                    case 1:
                        brush2 = Brushes.Black;
                        break;
                    case 2:
                        brush2 = Brushes.Red;
                        break;
                    case 3:
                        brush2 = Brushes.Blue;
                        break;
                    case 4:
                        brush2 = Brushes.Green;
                        break;
                }
            }
            //If line box is check, you will draw a Line
            if (this.lineButton.Checked)
            {
                this.objects.Add(new MyLine(this.startPoint, this.endPoint, pen));
            }
            //If Fill is checked and the pen is working (cannot draw with a broken pen)
            if (brush2 != null || pen != null)
            {
                //Draw a Rectangle
                if (this.rectangleButton.Checked)
                {
                    this.objects.Add(new MyRectangle(this.startPoint, this.endPoint, pen, brush2));
                }
                //Draw an Ellipse
                if (this.ellipseButton.Checked)
                {
                    this.objects.Add(new MyEllipse(this.startPoint, this.endPoint, pen, brush2));
                }
            }
            //If Text box is checked and it's not empty, draw the text in the box
            if (this.textButton.Checked && this.text.Text != "")
            {
                this.objects.Add(new MyText(this.text.Text, this.Font, brush, this.startPoint, this.endPoint));
            }
            //Update the drawPanel
            this.drawPanel.Invalidate();
            base.Update();
        }
    }
    //Create the base class MyObject
    //This base class doesn't do anything, it's just for easier refernece in drawPanel_Paint
    public class MyObject
    {
        public virtual void draw(Graphics g)
        {
        }
    }
    //Class for drawing Line
    public class MyLine : MyObject
    {
        private Point start;
        private Point end;
        private Pen pen;
        public MyLine(Point start, Point end, Pen pen)
        {
            this.start = start;
            this.end = end;
            this.pen = pen;
        }
        public override void draw(Graphics g)
        {
            g.DrawLine(this.pen, this.start, this.end);
        }
    }
    //Class for drawing Rectangle
    public class MyRectangle : MyObject
    {
        private Point start;
        private Point end;
        private Pen pen;
        private Brush brush;
        public MyRectangle(Point start, Point end, Pen pen, Brush brush)
        {
            this.start = start;
            this.end = end;
            this.pen = pen;
            this.brush = brush;
        }
        public override void draw(Graphics g)
        {
            int width = Math.Abs(this.end.X - this.start.X);
            int height = Math.Abs(this.end.Y - this.start.Y);
            int x = Math.Min(this.start.X, this.end.X);
            int y = Math.Min(this.start.Y, this.end.Y);
            if (this.brush != null)
            {
                g.FillRectangle(this.brush, x, y, width, height);
            }
            if (this.pen != null)
            {
                g.DrawRectangle(this.pen, x, y, width, height);
            }
        }
    }
    //Class for drawing Ellipse
    public class MyEllipse : MyObject
    {
        private Point start;
        private Point end;
        private Pen pen;
        private Brush brush;
        public MyEllipse(Point start, Point end, Pen pen, Brush brush)
        {
            this.start = start;
            this.end = end;
            this.pen = pen;
            this.brush = brush;
        }
        public override void draw(Graphics g)
        {
            int width = Math.Abs(this.end.X - this.start.X);
            int height = Math.Abs(this.end.Y - this.start.Y);
            int x = Math.Min(this.start.X, this.end.X);
            int y = Math.Min(this.start.Y, this.end.Y);
            if (this.brush != null)
            {
                g.FillEllipse(this.brush, x, y, width, height);
            }
            if (this.pen != null)
            {
                g.DrawEllipse(this.pen, x, y, width, height);
            }
        }
    }
    //Class for drawing Text
    public class MyText : MyObject
    {
        private Point start;
        private Point end;
        private Brush brush;
        private string s;
        private Font font;
        public MyText(string s, Font font, Brush brush, Point start, Point end)
        {
            this.start = start;
            this.end = end;
            this.brush = brush;
            this.font = font;
            this.s = s;
        }
        public override void draw(Graphics g)
        {
            int width = Math.Abs(this.end.X - this.start.X);
            int height = Math.Abs(this.end.Y - this.start.Y);
            int x = Math.Min(this.start.X, this.end.X);
            int y = Math.Min(this.start.Y, this.end.Y);
            g.DrawString(this.s, this.font, this.brush, new Rectangle(x, y, width, height));
        }
    }

}

