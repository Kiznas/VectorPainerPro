using System;

namespace Mods
{
    using global::VectorModderPack.VectorModderPack;
    using Mods.Properties;
    using System;
    using System.Drawing;
    using System.IO;

    namespace VectorModderPack
    {
        public class LineMod : IPaintable
        {
            public Bitmap Icon => Resources.line;

            public string ToolTitle => nameof(Resources.line);

            public (Point, Point) CheckIsFound(Point start, Point end, Point selection)
            {
                return (start, end);
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
