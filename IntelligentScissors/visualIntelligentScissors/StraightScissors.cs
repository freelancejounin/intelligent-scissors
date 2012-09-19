using System;
using System.Collections.Generic;
using System.Drawing;

namespace VisualIntelligentScissors
{
	public class StraightScissors : Scissors
	{
        Pen yellowpen = new Pen(Color.Yellow); 
		public StraightScissors() { }
		public StraightScissors(GrayBitmap image, Bitmap overlay) : base(image, overlay) { }

		public override void FindSegmentation(IList<Point> points, Pen pen)
		{
            if (Image == null) throw new InvalidOperationException("Set Image property first.");

            using (Graphics g = Graphics.FromImage(Overlay))
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Point start = points[i];
                    Point end = points[(i + 1) % points.Count];
                    g.DrawLine(pen, start, end);
                    g.DrawEllipse(yellowpen, start.X, start.Y, 5, 5);
                }
            }
            
		}
	}
}
