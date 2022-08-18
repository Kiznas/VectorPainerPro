using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPainerPro
{
    public partial class Form1 : Form
    {
        private bool _isClicked;
        private string _button;
        private Point _start;
        Bitmap _current;
        Bitmap _temp;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _temp = (Bitmap)pictureBox1.Image.Clone();
        }

        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrayPoints(int size)
            {
                if(size <= 0)
                {
                    size = 2;
                }

                points = new Point[size];
            }

            public void Setpoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }

            public void ResetPoints()
            {
                index = 0;
            }

            public int GetCountPoints()
            {
                return index;
            }

            public Point[] GetPoints()
            {
                return points;
            }
        }


        private ArrayPoints arrayPoints = new ArrayPoints(2);
        Pen pen = new Pen(Color.Black, 2f);

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if(openDllDialog.ShowDialog() == DialogResult.OK)
            {
                //load file
            }
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _isClicked = true;
            _start = e.Location;
            SaveUndo(_temp);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _isClicked = false;
            _temp = (Bitmap)pictureBox1.Image.Clone();
            arrayPoints.ResetPoints();
            SaveRedo(_start, e.Location, Pens.Black);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isClicked)
            {
                if (_button == "Line")
                {
                    using (var bitmap = new Bitmap(_temp, pictureBox1.Width, pictureBox1.Height))
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawLine(Pens.Black, _start, e.Location);
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = (Bitmap)bitmap.Clone();
                    }
                }
                else if (_button == "Pencil")
                {
                    Graphics graphics = Graphics.FromImage(_temp);
                    arrayPoints.Setpoint(e.X, e.Y);
                    if (arrayPoints.GetCountPoints() >= 2)
                    {
                        graphics.DrawLines(pen, arrayPoints.GetPoints());
                        pictureBox1.Image = _temp;
                        arrayPoints.Setpoint(e.X, e.Y);
                    }
                }
            }
        }
        private Bitmap _savedImg;
        private int _currentImg;
        private Point _x;
        private Point _y;
        private Pen _pen;
        private class Image
        {
            public Bitmap Img { get; set; }
            public Point X { get; set; }
            public Point Y { get; set; }
            public Pen Pen { get; set; }

            public Image(Bitmap image, Point x, Point y, Pen pen)
            {
                Img = image;
                X = x;
                Y = y;
                Pen = pen;
            }
        }

        public void SaveUndo(Bitmap image)
        {
            _savedImg = (Bitmap)image.Clone();
            new Image(_savedImg, _x, _y, _pen);
        }

        public void SaveRedo(Point x, Point y, Pen pen)
        {
            _x = x;
            _y = y;
            _pen = pen;
            new Image(_savedImg, _x, _y, _pen);
            _currentImg++;
        }

        public void Undo(object sender, EventArgs e)
        {
            using (var bitmap = new Bitmap(_savedImg, pictureBox1.Width, pictureBox1.Height))
            using (var graphics = Graphics.FromImage(_savedImg))
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bitmap.Clone();
                _temp = (Bitmap)pictureBox1.Image.Clone();
            }
        }

        public void Redo(object sender, EventArgs e)
        {
            if (_button == "Line")
            {
                using (var bitmap = new Bitmap(_temp, pictureBox1.Width, pictureBox1.Height))
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawLine(_pen, _x, _y);
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = (Bitmap)bitmap.Clone();
                }
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _button = "Line";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Redo(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Undo(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _button = "Pencil";
        }
    }
}
