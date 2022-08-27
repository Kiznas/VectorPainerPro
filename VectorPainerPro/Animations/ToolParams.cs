using System;

namespace VectorPainerPro
{
    public class ToolParams : ICloneable
    {
        public int LineWidth { get; set; }
        public int Color { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Update(AnimationAction animationAction)
        {
            StartX += animationAction.DeltaStartX;
            EndX += animationAction.DeltaEndX;
            StartY += animationAction.DeltaStartY;
            EndY += animationAction.DeltaEndY;
        }
    }
}
