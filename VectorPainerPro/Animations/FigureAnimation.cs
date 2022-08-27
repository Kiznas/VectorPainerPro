using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPainerPro
{
    public class FigureAnimation
    {
        public string ToolTitle { get; set; }
        public ToolParams ToolParams { get; set; }
        public IList<AnimationAction> AnimationActions { get; set; }
    }
}