using Mods.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using VectorModderPack.VectorModderPack;

namespace VectorModderPack
{
    public class RectangleMod : IPaintable
    {
        public Bitmap Icon => Resources.rectangle;

        public string ToolTitle => nameof(Resources.rectangle);
        static void Swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }

        public (Point, Point) CheckIsFound(Point start, Point end, Point selection)
        {
            bool isfound = false;

            if ((selection.X > start.X && selection.X < end.X) &&
                (selection.Y > start.Y && selection.Y < end.Y))
            {
                isfound = true;
            }

            if (isfound)
            {
                return (start, end);
            }
            else return (selection, selection);

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

            graphics.DrawRectangle(pen, startX, startY, width, height);
        }

        public void Fill(Graphics graphics, Brush brush, Point start, Point end)
        {
            throw new NotImplementedException();
        }
    }
}
