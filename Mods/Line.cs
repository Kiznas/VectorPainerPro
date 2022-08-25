using System;

namespace Mods
{
    using global::VectorModderPack.VectorModderPack;
    using Mods.Properties;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;

    namespace VectorModderPack
    {
        public class LineMod : IPaintable
        {
            public Bitmap Icon => Resources.line;

            public string ToolTitle => nameof(Resources.line);

            static void Swap<T>(ref T x, ref T y)
            {
                T t = y;
                y = x;
                x = t;
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

                if (selection.X > start.X && selection.X < end.X &&
                   selection.Y > start.Y && selection.Y < end.Y)
               {
                    var dxc = selection.X - start.X;
                    var dyc = selection.Y - start.Y;
                    var dxl = end.X - start.X;
                    var dyl = end.Y - start.Y;


                    var cross = dxc * dyl - dyc * dxl;

                    if (Math.Abs(cross) < 4000)
                    {
                        isfound = true;
                    }
               }


                if (isfound)
                {
                    return (start, end);
                }
                else return (selection, selection);

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
