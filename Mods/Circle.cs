using Mods.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using VectorModderPack.VectorModderPack;

namespace VectorModderPack
{
    public class CircleMod : IPaintable
    {
        public Bitmap Icon => Resources.circle;

        public string ToolTitle => nameof(Resources.circle);
        static void Swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }

        public (Point, Point) CheckIsFound(Point start, Point end, Point selection)
        {
            return (start, end);
        }

        public void Draw(Graphics graphics, Pen pen, Point start, Point end)
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

            int width = endX - startX;
            int height = endY - startY;

            graphics.DrawEllipse(pen, startX, startY, width, width);
        }

        public void Fill(Graphics graphics, Brush brush, Point start, Point end)
        {
            throw new NotImplementedException();
        }
    }
}
