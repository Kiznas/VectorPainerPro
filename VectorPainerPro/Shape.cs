using System;
using System.Collections.Generic;
using System.Drawing;
using static VectorPainerPro.MainForm;

namespace VectorPainerPro
{
    public class Shape
    {
        public Guid ShapeGuid { get; set; }

        public ToolType ToolType { get; set; }
        public string ToolName { get; set; } = String.Empty;
        public List<Point>? Points { get; set; }
        public int MainColor { get; set; }
        public int FillColor { get; set; }
        public float Width { get; set; }
    }
}
