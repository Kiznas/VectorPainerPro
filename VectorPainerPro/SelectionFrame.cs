using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class SelectionFrame
{
	public SelectionFrame()
	{
		
	}

	public void DrawSelectionFrame((Point, Point) frame, Bitmap bitmap, PictureBox pictureBox)
	{
        int frameRectangleSize = 6;

        var minX = frame.Item1.X - frameRectangleSize;
        var minY = frame.Item1.Y - frameRectangleSize;
        var maxX = frame.Item2.X;
        var maxY = frame.Item2.Y;

        int width = Math.Abs(frame.Item2.X - frame.Item1.X + frameRectangleSize);
        int height = Math.Abs(frame.Item2.Y - frame.Item1.Y + frameRectangleSize);
        var _selection = new Bitmap(bitmap);

        Rectangle[] frameRectangles = new Rectangle[8];

        using (var _bitmap = new Bitmap(_selection, pictureBox.Width, pictureBox.Height))
        {
            using (var graphics = Graphics.FromImage(_bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Pen pen = new(Color.Black, 1);
                SolidBrush blueBrush = new SolidBrush(Color.Black);

                frameRectangles[0] = new Rectangle(minX, minY, frameRectangleSize, frameRectangleSize);
                frameRectangles[1] = new Rectangle(minX + (width) / 2, minY, frameRectangleSize, frameRectangleSize);
                frameRectangles[2] = new Rectangle(maxX, minY, frameRectangleSize, frameRectangleSize);
                frameRectangles[3] = new Rectangle(minX, minY + (height) / 2, frameRectangleSize, frameRectangleSize);
                frameRectangles[4] = new Rectangle(maxX, minY + (height) / 2, frameRectangleSize, frameRectangleSize);
                frameRectangles[5] = new Rectangle(minX, maxY, frameRectangleSize, frameRectangleSize);
                frameRectangles[6] = new Rectangle(minX + (width) / 2, maxY, frameRectangleSize, frameRectangleSize);
                frameRectangles[7] = new Rectangle(maxX, maxY, frameRectangleSize, frameRectangleSize);
             
                foreach (var rectanle in frameRectangles)
                {
                    graphics.FillRectangle(blueBrush, rectanle);
                }

                pictureBox.Image?.Dispose();
                pictureBox.Image = (Bitmap)_bitmap.Clone();
            }
        }
    }
}
