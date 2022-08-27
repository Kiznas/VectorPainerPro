using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorModderPack.VectorModderPack;

namespace VectorPainerPro
{
    public partial class MainForm : Form
    {
        public enum ToolType
        {
            Select,
            Pencil,
            Mods
        }

        private ToolType currentToolType;
        private string currentToolName;
        private Point startPoint;

        private Action<Graphics, Pen, Point, Point>? DrawSomething;
        private Func<Point, Point, Point, (Point, Point)>? CheckIsFound;

        Bitmap _temp;

        ColorDialog colorDialog = new ColorDialog();
        Color newColor;
        Color newFillColor;
        Pen pen = new Pen(Color.Black, 1);

        public MainForm()
        {
            InitializeComponent();
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            _temp = (Bitmap)pictureBox.Image.Clone();
            SetStatusStripInfo();
        }

        public List<Shape> fileShapes = new List<Shape>();
        public List<Shape> fileShapesRedo = new List<Shape>();
        public Shape currentShape;
        public List<Point> shapePoints = new List<Point>();
        private ArrayPoints arrayPoints = new ArrayPoints(2);

        public IEnumerable<Type> assemblyTypes;

        public List<Tool> toolList = new List<Tool>();

        #region LoadTools
        private T GetPropertyFromType<T>(Type type, string propertyTitle, object instance)
        {
            var property = type
              .GetProperty(
                  propertyTitle,
                  BindingFlags.Public | BindingFlags.Instance);


            var propertyValue = property!.GetValue(instance);

            return (T)propertyValue!;
        }

        private void LoadTools()
        {
            if (openDllDialog.ShowDialog() == DialogResult.OK)
            {
                var assembly = Assembly.LoadFrom(openDllDialog.FileName);
                var types = assembly
                    .GetTypes()
                    .Where(x =>
                        x.GetInterface(typeof(IPaintable).FullName!) != null);

                foreach (var type in types)
                {
                    GenerateButton(type);
                }

                assemblyTypes = types;
            }
        }

        private void GenerateButton(Type type)
        {
            if (type.GetInterface(typeof(IPaintable).FullName!) == null)
            {
                throw new ArgumentException();
            }

            var obj = Activator.CreateInstance(type);
            if (obj != null)
            {
                var toolTitle = GetPropertyFromType<string>(type, nameof(IPaintable.ToolTitle), obj);
                var icon = GetPropertyFromType<Bitmap>(type, nameof(IPaintable.Icon), obj);
                var onClickMethod = type.GetMethod(nameof(IPaintable.Draw), BindingFlags.Public | BindingFlags.Instance);
                var checkIsFoundMethod = type.GetMethod(nameof(IPaintable.CheckIsFound), BindingFlags.Public | BindingFlags.Instance);

                if (onClickMethod != null)
                {
                    var toolAction = (Action<Graphics, Pen, Point, Point>)Delegate
                        .CreateDelegate(typeof(Action<Graphics, Pen, Point, Point>), obj, onClickMethod);

                    var toolIsFoundFunction = (Func<Point, Point, Point, (Point, Point)>)Delegate
                        .CreateDelegate(typeof(Func<Point, Point, Point, (Point, Point)>), obj, checkIsFoundMethod);

                    var onClick = new EventHandler((x, y) =>
                    {
                        currentToolType = ToolType.Mods;
                        currentToolName = toolTitle;
                        DrawSomething = toolAction;
                        CheckIsFound = toolIsFoundFunction;
                    });

                    var newTool = new Tool()
                    {
                        ToolAction = toolAction,
                        ToolName = toolTitle,
                        ToolIsFoundFunction = toolIsFoundFunction
                    };

                    toolList.Add(newTool);
                    ToolStripButton btnTool = new ToolStripButton(toolTitle, icon, onClick, toolTitle);
                    btnTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    btnTool.Text = toolTitle;

                    toolStripTools.Items.Add(btnTool);
                }
            }
        }

        #endregion

        #region СontrolsActions
        private void btnLoadTool_Click(object sender, EventArgs e)
        {
            LoadTools();
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
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
                _temp = (Bitmap)pictureBox.Image.Clone();
                pictureBox.Update();
                fileShapes.Clear();
            }
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
                fileShapes.Clear();
                string fileName = openFileDialog.FileName;
                if (fileName != null)
                {
                    _ = OpenFile(fileName);
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            currentToolType = ToolType.Select;
        }

        private void btnPencil_Click(object sender, EventArgs e)
        {
            currentToolType = ToolType.Pencil;
            currentToolName = "Pencil";
        }

        private void btnMainColor_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            newColor = colorDialog.Color;
            pen.Color = newColor;
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            newFillColor = colorDialog.Color;
            pen.Color = newFillColor;
        }

        private void trackBarLineThickness_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBarLineThickness.Value;
        }

        #endregion

        #region DrawProcess
        private new void MouseDown(object sender, MouseEventArgs e)
        {
            if (currentToolType != ToolType.Select)
            {
                arrayPoints.ResetPoints();
                startPoint = e.Location;
                CreateShape();
            }
            else
            {
                FindShape(e.Location);
            }
        }

        private new void MouseMove(object sender, MouseEventArgs e)
        {
            if (currentToolType != ToolType.Select)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (currentShape != null)
                    {
                        if (currentToolType == ToolType.Mods)
                        {
                            currentShape!.Points!.Clear();
                            currentShape!.Points!.Add(startPoint);

                        }
                        currentShape!.Points!.Add(e.Location);
                        DrawShape(currentShape);
                    }
                }
            }
        }

        private new void MouseUp(object sender, MouseEventArgs e)
        {
            if (currentToolType != ToolType.Select)
            {
                _temp = (Bitmap)pictureBox.Image.Clone();
                arrayPoints.ResetPoints();
            }
        }

        public void CreateShape()
        {
            var newShape = new Shape()
            {
                ShapeGuid = Guid.NewGuid(),
                ToolName = currentToolName,
                ToolType = currentToolType,
                Points = new List<Point>(),
                MainColor = pen.Color.ToArgb(),
                Width = pen.Width
            };

            currentShape = newShape;
            fileShapes.Add(newShape);
        }

        public void RedrawAll()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            _temp = (Bitmap)pictureBox.Image.Clone();
            pictureBox.Update();

            foreach (var shape in fileShapes)
            {
                DrawShape(shape);
            }
        }

        public void DrawShape(Shape shape)
        {
            if (currentToolType == ToolType.Pencil)
            {
                Graphics graphics = Graphics.FromImage(_temp);

                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                var point = shape.Points!.Last();
                {
                    arrayPoints.Setpoint(point.X, point.Y);

                    if (arrayPoints.GetCountPoints() >= 2)
                    {
                        graphics.DrawLines(pen, arrayPoints.GetPoints());
                        pictureBox.Image = _temp;
                        arrayPoints.Setpoint(point.X, point.Y);
                    }
                }
            }
            else if (currentToolType == ToolType.Mods)
            {
                using (var bitmap = new Bitmap(_temp, pictureBox.Width, pictureBox.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        pen.StartCap = LineCap.Flat;
                        pen.EndCap = LineCap.Flat;
                        pen.LineJoin = LineJoin.Miter;

                        var point = shape.Points!.Last();

                        DrawSomething?.Invoke(graphics, pen, startPoint, point);
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Bitmap)bitmap.Clone();
                    }
                }
            }
        }

        private void DrawFromList()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            _temp = (Bitmap)pictureBox.Image.Clone();

            pictureBox.Enabled = false;
            foreach (var shape in fileShapes)
            {
                DrawShapeFromList(shape);
            }
            pictureBox.Enabled = true;
        }

        private void DrawShapeFromList(Shape shape)
        {
            if (shape.ToolType == ToolType.Pencil)
            {
                foreach (var point in shape.Points!)
                {
                    using (var bitmap = new Bitmap(_temp, pictureBox.Width, pictureBox.Height))
                    {
                        Graphics graphics = Graphics.FromImage(_temp);

                        Pen pen = new(Color.FromArgb(shape.MainColor), shape.Width);
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        pen.LineJoin = LineJoin.Round;

                        {
                            arrayPoints.Setpoint(point.X, point.Y);
                            if (arrayPoints.GetCountPoints() >= 2)
                            {
                                graphics.DrawLines(pen, shape.Points.ToArray());
                                arrayPoints.Setpoint(point.X, point.Y);
                                pictureBox.Image = _temp;
                            }
                        }
                    }
                }
            }
            else
            {
                using (var bitmap = new Bitmap(_temp, pictureBox.Width, pictureBox.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        Pen pen = new(Color.FromArgb(shape.MainColor), shape.Width);
                        currentToolName = shape.ToolName;
                        DrawSomething = toolList!.Where(x => x.ToolName == currentToolName).Select(x => x.ToolAction).FirstOrDefault();

                        DrawSomething?.Invoke(graphics, pen, shape.Points.First(), shape.Points.Last());
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Bitmap)bitmap.Clone();
                        _temp = (Bitmap)pictureBox.Image.Clone();
                    }
                }
            }
        }

        #endregion

        #region UndoRedoactions
        private void Undo()
        {
            pictureBox.Enabled = false;
            if (fileShapes.Count > 0)
            {
                fileShapesRedo.Add(fileShapes.Last());
                fileShapes.RemoveAt(fileShapes.Count - 1);
                DrawFromList();
            }
            pictureBox.Enabled = true;
        }

        private void Redo()
        {
            pictureBox.Enabled = false;
            if (fileShapesRedo.Count > 0)
            {
                fileShapes.Add(fileShapesRedo.Last());
                fileShapesRedo.RemoveAt(fileShapesRedo.Count - 1);
                DrawFromList();
            }
            pictureBox.Enabled = true;
        }
        #endregion

        #region FramesPart
        private void FindShape(Point point)
        {
            var found = false;
            (Point, Point) frame = (point, point);
            Shape foundShape = new Shape();


            foreach (var shape in fileShapes)
            {
                if (shape.ToolType == ToolType.Pencil)
                {
                    //found = false;
                    foreach (var shapePoint in shape.Points)
                    {
                        if (Math.Abs(shapePoint.X - point.X) < 10 && Math.Abs(shapePoint.Y - point.Y) < 10)
                        {
                            found = true;

                            var minX = shape.Points.Select(x => x.X).Min();
                            var minY = shape.Points.Select(x => x.Y).Min();
                            var maxX = shape.Points.Select(x => x.X).Max();
                            var maxY = shape.Points.Select(x => x.Y).Max();

                            var framePointStart = new Point(minX, minY);
                            var framePointEnd = new Point(maxX, maxY);

                            frame = (framePointStart, framePointEnd);
                            break;
                        }
                    }
                }
                else
                {
                    CheckIsFound = toolList!.Where(x => x.ToolName == shape.ToolName).Select(x => x.ToolIsFoundFunction).FirstOrDefault();
                    var result = (CheckIsFound?.Invoke(shape.Points[0], shape.Points[1], point));

                    frame = (result!.Value.Item1, result!.Value.Item2);
                    found = frame.Item1 != point || frame.Item2 != point;
                    if (found)
                    {
                        break;
                    }
                }
            }
            if (found)
            {
                DrawSelectionFrame(frame);
                found = false;
            }
            else
            {
                using (var bitmap = new Bitmap(_temp, pictureBox.Width, pictureBox.Height))
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = (Bitmap)bitmap.Clone();
                    found = false;
                }
            }
        }

        private void DrawSelectionFrame((Point, Point) frame)
        {

            var minX = frame.Item1.X;
            var minY = frame.Item1.Y;
            var maxX = frame.Item2.X;
            var maxY = frame.Item2.Y;

            int width = Math.Abs(frame.Item2.X - frame.Item1.X);
            int height = Math.Abs(frame.Item2.Y - frame.Item1.Y);

            var _selection = new Bitmap(_temp);

            using (var bitmap = new Bitmap(_selection, pictureBox.Width, pictureBox.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    Pen pen = new(Color.Black, 1);
                    SolidBrush blueBrush = new SolidBrush(Color.Black);

                    Rectangle rect11 = new Rectangle(minX - 6, minY - 6, 6, 6);
                    Rectangle rect12 = new Rectangle(minX - 6 + (width) / 2, minY - 6, 6, 6);
                    Rectangle rect13 = new Rectangle(maxX, minY - 6, 6, 6);

                    Rectangle rect21 = new Rectangle(minX - 6, minY + (height) / 2, 6, 6);
                    Rectangle rect23 = new Rectangle(maxX, minY + (height) / 2, 6, 6);

                    Rectangle rect31 = new Rectangle(minX - 6, maxY, 6, 6);
                    Rectangle rect32 = new Rectangle(minX - 6 + (width) / 2, maxY, 6, 6);
                    Rectangle rect33 = new Rectangle(maxX, maxY, 6, 6);

                    graphics.FillRectangle(blueBrush, rect11);
                    graphics.FillRectangle(blueBrush, rect12);
                    graphics.FillRectangle(blueBrush, rect13);

                    graphics.FillRectangle(blueBrush, rect21);
                    graphics.FillRectangle(blueBrush, rect23);

                    graphics.FillRectangle(blueBrush, rect31);
                    graphics.FillRectangle(blueBrush, rect32);
                    graphics.FillRectangle(blueBrush, rect33);

                    pictureBox.Image?.Dispose();
                    pictureBox.Image = (Bitmap)bitmap.Clone();
                }
            }



        }
        #endregion

        #region SaveOpen
        private void SaveFile(string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(fileShapes, options);
            System.IO.File.WriteAllText(fileName, jsonString);
        }

        private async Task OpenFile(string fileName)
        {
            using FileStream openStream = File.OpenRead(fileName);
            List<Shape>? loadedShapes = new List<Shape>();
            loadedShapes = await JsonSerializer.DeserializeAsync<List<Shape>>(openStream);
            if (loadedShapes != null)
            {

                var loadedToolNames = toolList.Select(x => x.ToolName).ToList();
                if (loadedShapes.Where(x => x.ToolType == ToolType.Mods && !loadedToolNames.Contains(x.ToolName)).Any())
                {
                    string caption = "Error Loading File";
                    string message = "File contains unsupported elements!";

                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                }
                else
                {
                    fileShapes = loadedShapes;
                    DrawFromList();
                }
            }
        }
        #endregion

        #region Animation
        CancellationTokenSource cancelationTokenSource;
        bool wasStopped;
        bool cycled;
        FigureAnimation figureAnimation;
        ToolParams currentParams;
        int currentAnimationIndex;
        int currentRepeatIndex;

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (!wasStopped)
            {
                using (StreamReader sr = new StreamReader(
                    @"C:\Users\Klimt\source\repos\ClockAnim.json"))
                {
                    figureAnimation = JsonSerializer.Deserialize<FigureAnimation>(sr.ReadToEnd());
                }

                currentParams = (ToolParams)figureAnimation.ToolParams.Clone();
                currentAnimationIndex = 0;
                currentRepeatIndex = 0;
            }

            cancelationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(() => Test(cancelationTokenSource.Token));
            thread.Start();
        }

        private void Test(CancellationToken cancellationToken)
        {
            do
            {
                using (var pen = new Pen(
                    Color.FromArgb(figureAnimation.ToolParams.Color), figureAnimation.ToolParams.LineWidth))
                {
                    for (; currentAnimationIndex < figureAnimation.AnimationActions.Count; ++currentAnimationIndex)
                    {
                        for (; currentRepeatIndex < figureAnimation.AnimationActions[currentAnimationIndex].Count; ++currentRepeatIndex)
                        {
                            using (var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height))
                            {
                                using (var graphics = Graphics.FromImage(bitmap))
                                {
                                    graphics.DrawEllipse(pen,
                                       (pictureBox.Width / 2) - 50,
                                       (pictureBox.Height / 2) - 50,
                                       100,
                                       100);

                                    graphics.DrawLine(
                                       pen,
                                       currentParams.StartX,
                                       currentParams.StartY,
                                       currentParams.EndX,
                                       currentParams.EndY);

                                    currentParams.Update(figureAnimation.AnimationActions[currentAnimationIndex]);
                                    Thread.Sleep(10);
                                    pictureBox.Image?.Dispose();
                                    pictureBox.Image = (Bitmap)bitmap.Clone();

                                    if (cancellationToken.IsCancellationRequested)
                                    {
                                        wasStopped = true;
                                        return;
                                    }
                                }
                            }
                        }

                        currentRepeatIndex = 0;
                    }

                    currentAnimationIndex = 0;
                    currentParams = (ToolParams)figureAnimation.ToolParams.Clone();
                }
            } while (cycled == true);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            cancelationTokenSource.Cancel();
        }

        private void buttonRepeat_Click(object sender, EventArgs e)
        {
            if(cycled == true)
            {
                cycled = false;
                buttonRepeat.BackColor = Color.Red;
            }
            else
            {
                cycled = true;
                buttonRepeat.BackColor = Color.Green;
            }
        }

        private void buttonCreateAnimation_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var widthCenter = pictureBox.Width / 2;
            var heightCenter = pictureBox.Height / 2;
            var animationActions = new List<AnimationAction>();
            for (int i = 0; i < 1; i++)
            {
                animationActions.Add(new AnimationAction
                {
                    Count = 50,
                    DeltaEndX = 1,
                    DeltaEndY = 1
                });
                animationActions.Add(new AnimationAction
                {
                    Count = 50,
                    DeltaEndX = -1,
                    DeltaEndY = 1
                });
                animationActions.Add(new AnimationAction
                {
                    Count = 50,
                    DeltaEndX = -1,
                    DeltaEndY = -1
                });
                animationActions.Add(new AnimationAction
                {
                    Count = 50,
                    DeltaEndX = 1,
                    DeltaEndY = -1
                });
            }

            var animation = new FigureAnimation
            {
                ToolTitle = nameof(Graphics.DrawRectangle),
                ToolParams = new ToolParams
                {
                    Color = Color.FromArgb(
                        random.Next(0, 255),
                        random.Next(0, 255),
                        random.Next(0, 255)).ToArgb(),
                    LineWidth = 2,
                    StartX = widthCenter,
                    EndX = widthCenter,
                    StartY = heightCenter,
                    EndY = heightCenter - 50
                },
                AnimationActions = animationActions
            };

            var filepath = Path.GetTempFileName();
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.Write(JsonSerializer.Serialize(animation));
            }
            MessageBox.Show(filepath);

        }
        #endregion

        private void SetStatusStripInfo()
        {
            statusLabelCanvaSize.Text = "Image Size " + pictureBox.Width + " : " + pictureBox.Height;
        }
    }
}
