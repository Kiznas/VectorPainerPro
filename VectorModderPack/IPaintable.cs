using System;

namespace VectorModderPack
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace VectorModderPack
    {
        public interface IPaintable
        {
            Bitmap Icon { get; }
            string ToolTitle { get; }
            void Draw(Graphics graphics, Pen pen, Point start, Point end);
            void Fill(Graphics graphics, Brush brush, Point start, Point end);
        }
    }
}
