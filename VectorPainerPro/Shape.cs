using System;
using System.Drawing;

namespace VectorPainerPro
{
    public class Shape
    {
        public Guid ShapeGuid { get; set; }
        public string Tool { get; set; } = String.Empty;
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color MainColor { get; set; }
        public Color FillColor { get; set; }
        public float Width { get; set; }
    }
}
