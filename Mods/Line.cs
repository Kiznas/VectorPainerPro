using System;

namespace Mods
{
    using global::VectorModderPack.VectorModderPack;
    using Mods.Properties;
    using System;
    using System.Drawing;

    namespace VectorModderPack
    {
        public class LineMod : IPaintable
        {
            public Bitmap Icon => Resources.line;

            public string ToolTitle => nameof(Resources.line);

            static void Swap<T>(ref T x, ref T y)
            {
                (x, y) = (y, x);
            }

            public (Point, Point) CheckIsFound(Point start, Point end, Point selection)
            {
                bool isfound = false;

                int startX = start.X;
                int startY = start.Y;
                int endX = end.X;
                int endY = end.Y;

                if (startX > endX)
                {
                    Swap(ref startX, ref endX);
                }

                if (startY > endY)
                {
                    Swap(ref startY, ref endY);
                }

                if (selection.X > startX && selection.X < endX &&
                   selection.Y > startY && selection.Y < endY)
                {
                    var dxc = selection.X - startX;
                    var dyc = selection.Y - startY;
                    var dxl = endX - startX;
                    var dyl = endY - startY;

                    var cross = dxc * dyl - dyc * dxl;

                    isfound = Math.Abs(cross) < 2000;
                }

                if (isfound)
                {
                    Point point1 =
                        new Point(startX, startY);
                    Point point2 =
                        new Point(endX, endY);
                    
                    return (point1, point2);
                }
                else
                {
                    return (selection, selection);
                }
            }

            public (Point, Point) GetSelectionFrame(Point start, Point end)
            {
                int startX = start.X;
                int startY = start.Y;
                int endX = end.X;
                int endY = end.Y;

                if (startX > endX)
                {
                    Swap(ref startX, ref endX);
                }

                if (startY > endY)
                {
                    Swap(ref startY, ref endY);
                }

                Point point1 =
                    new Point(startX, startY);
                Point point2 =
                    new Point(endX, endY);
                
                return (point1, point2);

            }

            public void Draw(Graphics graphics, Pen pen, Point start, Point end)
            {
                graphics.DrawLine(pen, start, end);
            }

            public void Fill(Graphics graphics, Brush brush, Point start, Point end)
            {
                throw new NotImplementedException();
            }
        }
    }
}
