using Mods.Properties;
using System;
using System.Collections.Generic;
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
            (x, y) = (y, x);
        }

        public (Point, Point) CheckIsFound(Point start, Point end, Point selection)
        {
            int distX = Math.Abs(end.X - start.X);
            int radius = distX / 2;

            Point center = new Point(start.X + radius, start.Y + radius);

            bool isfound = Math.Pow(selection.X - center.X, 2) + Math.Pow(selection.Y - center.Y, 2) < Math.Pow(radius, 2);

            if (isfound)
            {
                Point point1 =
                    new Point(start.X, start.Y);
                Point point2 =
                    new Point(start.X + 2 * radius, start.Y + 2 * radius);
                return (point1, point2);
            }
            else
            {
                return (selection, selection);
            }
        }

        public (Point, Point) GetSelectionFrame(Point start, Point end)
        {
            int distX = Math.Abs(end.X - start.X);
            int radius = distX / 2;

            Point point1 =
                new Point(start.X, start.Y);
            Point point2 =
                new Point(start.X + 2 * radius, start.Y + 2 * radius);

            return (point1, point2);
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
