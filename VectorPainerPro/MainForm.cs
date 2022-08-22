using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorModderPack.VectorModderPack;

namespace VectorPainerPro
{
    public partial class MainForm : Form
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

        public MainForm()
        {
            InitializeComponent();
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            _temp = (Bitmap)pictureBox.Image.Clone();
            SaveUndo(_temp);
            SetStatusStripInfo();
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
        
        public List<Shape> fileShapes = new List<Shape>();
        public Shape currentShape;

        private void btnLoadTool_Click(object sender, EventArgs e)
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

            ToolStripButton btnTool = new ToolStripButton(toolTitle, icon, onClick, toolTitle);
            btnTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnTool.Text = toolTitle;

            toolStripTools.Items.Add(btnTool);
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

        private new void MouseDown(object sender, MouseEventArgs e)
        {
            _isClicked = true;
            _start = e.Location;
        }

        private new void MouseUp(object sender, MouseEventArgs e)
        {
            _isClicked = false;
            _temp = (Bitmap)pictureBox.Image.Clone();
            arrayPoints.ResetPoints();
            SaveRedo(_temp);

            var newShape = new Shape()
            {
                ShapeGuid = Guid.NewGuid(),
                Tool = "brake"
            };
            currentShape = newShape;
            fileShapes.Add(newShape);


            //if (currentShape != null)
            //{
            //    SaveShape(currentShape);
            //}
        }

        private new void MouseMove(object sender, MouseEventArgs e)
        {
            if (_isClicked && _currentTool == "pencil")
            {
                Graphics graphics = Graphics.FromImage(_temp);
                arrayPoints.Setpoint(e.X, e.Y);
                if (arrayPoints.GetCountPoints() >= 2)
                {
                    graphics.DrawLines(pen, arrayPoints.GetPoints());
                    pictureBox.Image = _temp;
                    arrayPoints.Setpoint(e.X, e.Y);
                    
                    var newShape = new Shape()
                    {
                        ShapeGuid = Guid.NewGuid(),
                        Tool = "pencil",
                        StartPoint = arrayPoints.GetPoints()[0],
                        EndPoint = arrayPoints.GetPoints()[1],
                        MainColor = pen.Color,
                        Width = pen.Width
                    };
                    currentShape = newShape;
                    fileShapes.Add(newShape);
                    currentShape = null;
                }
                
            }

            else if (_isClicked)
            {
                using (var bitmap = new Bitmap(_temp, pictureBox.Width, pictureBox.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        DrawSomething?.Invoke(graphics, pen, _start, e.Location);
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Bitmap)bitmap.Clone();

                        var newShape = new Shape()
                        {
                            ShapeGuid = Guid.NewGuid(),
                            Tool = _currentTool,
                            StartPoint = _start,
                            EndPoint = e.Location,
                            MainColor = pen.Color,
                            Width = pen.Width
                        };
                        currentShape = newShape;
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

        public void SaveShape(Shape shape)
        {
            fileShapes.Add(shape);
        }

        public void Undo(object sender, EventArgs e)
        {
            using (var bitmap = new Bitmap(imgs[--_localCount], pictureBox.Width, pictureBox.Height))
            using (var graphics = Graphics.FromImage(imgs[_localCount]))
            {
                pictureBox.Image = (Bitmap)bitmap.Clone();
                _temp = (Bitmap)pictureBox.Image.Clone();
            }
        }

        public void Redo(object sender, EventArgs e)
        {
            using (var bitmap = new Bitmap(imgs[++_localCount], pictureBox.Width, pictureBox.Height))
            using (var graphics = Graphics.FromImage(imgs[_localCount]))
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = (Bitmap)bitmap.Clone();
                _temp = (Bitmap)pictureBox.Image.Clone();
            }
        }
        // Undo/Redo part

        private void btnUndo_Click(object sender, EventArgs e)
        {

            Undo(sender, e);
            if (_localCount == imgs.Count - 1)
            {
                btnUndo.Enabled = true;
                btnRedo.Enabled = false;
            }
            else if (_localCount == 0)
            {
                btnUndo.Enabled = false;
                btnRedo.Enabled = true;
            }
            else
            {
                btnUndo.Enabled = true;
                btnRedo.Enabled = true;
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo(sender, e);
            if (_localCount == imgs.Count - 1)
            {
                btnUndo.Enabled = true;
                btnRedo.Enabled = false;
            }
            else if (_localCount == 0)
            {
                btnUndo.Enabled = false;
                btnRedo.Enabled = true;
            }
            else
            {
                btnUndo.Enabled = true;
                btnRedo.Enabled = true;
            }
        }

        private void btnPencil_Click(object sender, EventArgs e)
        {
            _currentTool = "pencil";
            btnPencil.Checked = true;
        }

        private void btnMainColor_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pen.Color = new_color;
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pen.Color = new_color;
        }

        private void trackBarLineThickness_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBarLineThickness.Value;
        }

        private void labelMainColor_Click(object sender, EventArgs e)
        {

        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            const string message = "Are you sure that you would like to create new file?";
            const string caption = "Creating New file";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                pictureBox.Image = null;
                pictureBox.Update();
                fileShapes.Clear();
            }
            
        }

        private void SetStatusStripInfo()
        {
            statusLabelCanvaSize.Text = "Image Size " + pictureBox.Width + " : " + pictureBox.Height;
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json files | *.json";
            saveFileDialog.DefaultExt = "json";

            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                SaveFile(fileName);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json files | *.json";
            openFileDialog.DefaultExt = "json";

            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                OpenFile(fileName);
            }
        }

        private void SaveFile(string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(fileShapes, options);
            System.IO.File.WriteAllText(fileName, jsonString);
        }

        private async Task OpenFile(string fileName)
        {
            using FileStream openStream = File.OpenRead(fileName);
            List<Shape> loadedShapes = new List<Shape>();
            loadedShapes = await JsonSerializer.DeserializeAsync<List<Shape>>(openStream);
            if (loadedShapes != null)
            {
                fileShapes = loadedShapes;
                DrawFromFile();
            }              
        }

        private void DrawFromFile()
        {
            pictureBox.Enabled = false;
            foreach (var shape in fileShapes)
            {
                DrawShape(shape);
            }
            pictureBox.Enabled = true;
        }

        private void DrawShape(Shape shape)
        {
            if (shape.Tool == "brake")
            {              
                Graphics graphics = Graphics.FromImage(_temp);
                {
                    arrayPoints.ResetPoints();
                }
            }
            if (shape.Tool == "pencil")
            {               
                Graphics graphics = Graphics.FromImage(_temp);
                {
                    arrayPoints.Setpoint(shape.StartPoint.X, shape.StartPoint.Y);
                    if (arrayPoints.GetCountPoints() >= 2)
                    {
                        graphics.DrawLines(pen, arrayPoints.GetPoints());
                        arrayPoints.Setpoint(shape.EndPoint.X, shape.EndPoint.Y);
                        pictureBox.Image = _temp;
                    }
                }
            }

            else 
            {
                using (var bitmap = new Bitmap(_temp, pictureBox.Width, pictureBox.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        Pen pen = new(shape.MainColor, shape.Width);
                        _currentTool = shape.Tool;

                        DrawSomething?.Invoke(graphics, pen, shape.StartPoint, shape.EndPoint);
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Bitmap)bitmap.Clone();
                    }
                }
            }
        }

    }
}
