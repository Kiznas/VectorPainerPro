using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            SaveUndo(_temp);
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
        Pen pen = new Pen(Color.Black, 3f);

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
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _isClicked = false;
            _temp = (Bitmap)pictureBox1.Image.Clone();
            arrayPoints.ResetPoints();
            SaveRedo(_temp);
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
        private int _localCount;
        private class Image
        {
            public Bitmap Img { get; set; }

            public Image(Bitmap image)
            {
                Img = image;
            }
        }
        private List<Bitmap> imgs = new List<Bitmap>();
        public void Save(Bitmap img)
        {
            imgs.Add(img);
        }

        public void SaveUndo(Bitmap image)
        {
            _savedImg = (Bitmap)image.Clone();
            Save(_savedImg);
        }

        public void SaveRedo(Bitmap image)
        {
            _savedImg = (Bitmap)image.Clone();
            Save(_savedImg);
            _currentImg++;
            _localCount = _currentImg;
        }
        public void Undo(object sender, EventArgs e)
        {
            using (var bitmap = new Bitmap(imgs[--_localCount], pictureBox1.Width, pictureBox1.Height))
            using (var graphics = Graphics.FromImage(imgs[_localCount]))
            {
                pictureBox1.Image = (Bitmap)bitmap.Clone();
                _temp = (Bitmap)pictureBox1.Image.Clone();
            }
        }

        public void Redo(object sender, EventArgs e)
        {
            using (var bitmap = new Bitmap(imgs[++_localCount], pictureBox1.Width, pictureBox1.Height))
            using (var graphics = Graphics.FromImage(imgs[_localCount]))
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bitmap.Clone();
                _temp = (Bitmap)pictureBox1.Image.Clone();
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _button = "Line";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Redo(sender, e);
            if (_localCount == imgs.Count - 1)
            {
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = false;
            }
            else if (_localCount == 0)
            {
                toolStripButton3.Enabled = false;
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = true;
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Undo(sender, e);
            if (_localCount == imgs.Count - 1)
            {
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = false;
            }
            else if (_localCount == 0)
            {
                toolStripButton3.Enabled = false;
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = true;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _button = "Pencil";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
