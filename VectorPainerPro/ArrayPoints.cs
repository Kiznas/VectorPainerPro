using System;
using System.Drawing;

namespace VectorPainerPro
{
    public class ArrayPoints
    {
        private int index = 0;
        private Point[] points;

        public ArrayPoints(int size)
        {
            if (size <= 0)
            {
                size = 2;
            }

            points = new Point[size];
        }

        public void Setpoint(int x, int y)
        {
            if (index >= points.Length)
            {
                index = 0;
            }
            points[index] = new Point(x, y);
            index++;
        }

        public void ResetPoints()
        {
            index = 0;
        }

        public int GetCountPoints()
        {
            return index;
        }

        public Point[] GetPoints()
        {
            return points;
        }
    }
}
