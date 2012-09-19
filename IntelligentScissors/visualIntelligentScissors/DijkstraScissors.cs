using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Collections; 

namespace VisualIntelligentScissors
{
	public class DijkstraScissors : Scissors
	{
        private Pen pen = new Pen(Color.Yellow);
        private Font font = new Font("Arial", 10);
        private Brush brush = new SolidBrush(Color.SteelBlue);
        private Pen blackpen = new Pen(Color.Black, 3);
        private PriorityQueue priorityQueue;
        private IntelligentPixel[,] allPoints;

		public DijkstraScissors() { }
        /// <summary>
        /// constructor for intelligent scissors. 
        /// </summary>
        /// <param name="image">the image you are oging to segment.  has methods for getting gradient information</param>
        /// <param name="overlay">an overlay on which you can draw stuff by setting pixels.</param>
		public DijkstraScissors(GrayBitmap image, Bitmap overlay) : base(image, overlay) { }

        // this is the class you need to implement in CS 312

        /// <summary>
        /// this is the class you implement in CS 312. 
        /// </summary>
        /// <param name="points">these are the segmentation points from the pgm file.</param>
        /// <param name="pen">this is a pen you can use to draw on the overlay</param>
		public override void FindSegmentation(IList<Point> points, Pen pen)
		{
			if (Image == null) throw new InvalidOperationException("Set Image property first.");
            // this is the entry point for this class when the button is clicked
            // to do the image segmentation with intelligent scissors.
            Program.MainForm.RefreshImage();
            GetPixelWeight(points[1]);

            using (Graphics g = Graphics.FromImage(Overlay))
            {
                //add all points to an array of IntelligentPixels
                allPoints = new IntelligentPixel[Overlay.Width + 1, Overlay.Height + 1];
                for (int i = 0; i < Overlay.Width; i++)
                {
                    for (int j = 0; j < Overlay.Height; j++)
                    {
                        allPoints[i, j] = new IntelligentPixel(i, j);
                    }
                }
                //draw all selected points
                for (int p = 0; p < points.Count; p++)
                {
                    Point temp = points[p];
                    g.DrawEllipse(pen, temp.X, temp.Y, 5, 5);
                }
                Program.MainForm.RefreshImage();

                //for all selected points
                for (int i = 0; i < points.Count; i++)
                {
                    priorityQueue = new PriorityQueue();

                    Point start = points[i];
                    IntelligentPixel startP = allPoints[start.X, start.Y];
                    startP.Prev = null;
                    Point end = points[(i + 1) % points.Count];
                    IntelligentPixel endP = allPoints[end.X, end.Y];
                    endP.Prev = null;
                    IntelligentPixel curP = startP;
                    curP.Priority = 0;
                    priorityQueue.Add(startP);

                    while (!(curP.Equals(endP)) && (priorityQueue.hasNextPixel()))
                    {
                        curP = priorityQueue.nextPixel;
                        curP.Visited = true;
                        int cost = curP.Priority;
                        
                        //draw points during search
                        //Overlay.SetPixel(curP.X, curP.Y, Color.Green);
                        //Program.MainForm.RefreshImage();

                        IntelligentPixel up = allPoints[curP.X, curP.Y - 1];
                        if (up.Y > 0)
                            CheckAndAdd(up, cost, curP);
                        
                        IntelligentPixel right = allPoints[curP.X + 1, curP.Y];
                        if (right.X < Overlay.Width - 1)
                            CheckAndAdd(right, cost, curP);

                        IntelligentPixel left = allPoints[curP.X - 1, curP.Y];
                        if (left.X > 0)
                            CheckAndAdd(left, cost, curP);

                        IntelligentPixel down = allPoints[curP.X, curP.Y + 1];
                        if (down.Y < Overlay.Height - 1)
                            CheckAndAdd(down, cost, curP);
                    }

                    if (curP.Equals(endP))
                    {
                        IntelligentPixel backTracker = endP;

                        while (backTracker.Prev != null)
                        {
                            Overlay.SetPixel(backTracker.X, backTracker.Y, Color.Red);
                            backTracker = backTracker.Prev;
                        }
                    }


                    IntelligentPixel tIP;
                    for (int row = 0; row < Overlay.Width; row++)
                    {
                        for (int col = 0; col < Overlay.Height; col++)
                        {
                            tIP = allPoints[row, col];
                            tIP.Visited = false;
                            tIP.Priority = 999999;                            
                        }
                    }

                }
                Program.MainForm.RefreshImage();
            }
        }


        public void CheckAndAdd(IntelligentPixel ip, int cost, IntelligentPixel prev)
        {
            if (!ip.Visited)
            {
                IntelligentPixel temp = allPoints[ip.X, ip.Y];
                int tempP = cost + GetPixelWeight(new Point(ip.X, ip.Y));

                if (priorityQueue.Contains(temp))
                {
                    if (tempP < temp.Priority)
                    {
                        temp.Priority = tempP;
                        temp.Prev = prev;
                    }
                }
                else
                {
                    temp.Priority = tempP;
                    temp.Prev = prev;
                    priorityQueue.Add(temp);
                }
            }
        }
    }

    
}

