using System;
using System.Collections.Generic;
using System.Drawing;

namespace VectorPainerPro
{
    public class Tool
    {
        public string ToolName { get; set; } = string.Empty;
        public Action<Graphics, Pen, Point, Point>? ToolAction { get; set; }
        public Func<Point, Point, Point, (Point, Point)>? IsFoundFunction { get; set; }
        public Func<Point, Point, (Point, Point)>? GetSelectionFrameFunction { get; set; }
}
}
