
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorModderPack.VectorModderPack;

namespace VectorPainerPro
{
    public partial class Form1 : Form
    {
        private bool _isClicked;
        private string _currentTool;
        private Point _start;
        private Color color;
        private Action<Graphics, Pen, Point, Point> DrawSomething;
        Bitmap _temp;
        ColorDialog cd = new ColorDialog();
        Color new_color;
        Pen pen = new Pen(Color.Black, 1);

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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (openDllDialog.ShowDialog() == DialogResult.OK)
            {
                var assembly = Assembly.LoadFrom(openDllDialog.FileName);
                var types = assembly
                    .GetTypes()
                    .Where(x =>
                        x.GetInterface(typeof(IPaintable).FullName) != null);

                foreach (var type in types)
                {
                    GenerateButton(type);
                }
            }
        }

        private void GenerateButton(Type type)
        {
            if (type.GetInterface(typeof(IPaintable).FullName) == null)
            {
                throw new ArgumentException();
            }

            var obj = Activator.CreateInstance(type);
            var toolTitle = GetPropertyFromType<string>(type, nameof(IPaintable.ToolTitle), obj);
            var icon = GetPropertyFromType<Bitmap>(type, nameof(IPaintable.Icon), obj);

            var onClickMethod = type.GetMethod(nameof(IPaintable.Draw), BindingFlags.Public | BindingFlags.Instance);
            var action = (Action<Graphics, Pen, Point, Point>)Delegate
                .CreateDelegate(typeof(Action<Graphics, Pen, Point, Point>), obj, onClickMethod);

            var onClick = new EventHandler((x, y) =>
            {
                _currentTool = toolTitle;
                DrawSomething = action;
            });

            ToolStripButton toolStripButton = new ToolStripButton(toolTitle, icon, onClick, toolTitle);

            toolStripTools.Items.Add(toolStripButton);
        }

        private T GetPropertyFromType<T>(Type type, string propertyTitle, object instance)
        {
            var property = type
              .GetProperty(
                  propertyTitle,
                  BindingFlags.Public | BindingFlags.Instance);

            var propertyValue = property.GetValue(instance);

            return (T)propertyValue;
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
            if (_isClicked && _currentTool == "pencil")
            {
                Graphics graphics = Graphics.FromImage(_temp);
                arrayPoints.Setpoint(e.X, e.Y);
                if (arrayPoints.GetCountPoints() >= 2)
                {
                    graphics.DrawLines(pen, arrayPoints.GetPoints());
                    pictureBox1.Image = _temp;
                    arrayPoints.Setpoint(e.X, e.Y);
                }            }
            else if (_isClicked)
            {
                using (var bitmap = new Bitmap(_temp, pictureBox1.Width, pictureBox1.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        DrawSomething?.Invoke(graphics, pen, _start, e.Location);
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = (Bitmap)bitmap.Clone();
                    }
                }
            }
        }

        // Undo/Redo part
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
        // Undo/Redo part

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

            Undo(sender, e);
            if (_localCount == imgs.Count - 1)
            {
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = false;
            }
            else if (_localCount == 0)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = true;
            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Redo(sender, e);
            if (_localCount == imgs.Count - 1)
            {
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = false;
            }
            else if (_localCount == 0)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = true;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _currentTool = "pencil";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pen.Color = new_color;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
    }
}
