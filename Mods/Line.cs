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
            public Bitmap Icon => Resources.Line;

            public string ToolTitle => "Line";

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
