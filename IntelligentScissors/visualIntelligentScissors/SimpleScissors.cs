using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;

namespace VisualIntelligentScissors
{
	public class SimpleScissors : Scissors
	{
        private Pen pen = new Pen(Color.Yellow);
        private Font font = new Font("Arial", 10);
        private Brush brush = new SolidBrush(Color.SteelBlue);
        private Pen blackpen = new Pen(Color.Black, 3);
        private Color[] colors;

		public SimpleScissors() { }

        /// <summary>
        /// constructor for SimpleScissors. 
        /// </summary>
        /// <param name="image">the image you are going to segment including methods for getting gradients.</param>
        /// <param name="overlay">a bitmap on which you can draw stuff.</param>
		public SimpleScissors(GrayBitmap image, Bitmap overlay) : base(image, overlay) {
            
        }

        // this is a class you need to implement in CS 312. 

        /// <summary>
        ///  this is the class to implement for CS 312. 
        /// </summary>
        /// <param name="points">the list of segmentation points parsed from the pgm file</param>
        /// <param name="pen">a pen for writing on the overlay if you want to use it.</param>
		public override void FindSegmentation(IList<Point> points, Pen pen)
		{
            // this is the entry point for this class when the button is clicked for 
            // segmenting the image using the simple greedy algorithm. 
            // the points
            //interesting start points (135, 63); (242, 28)
            Random ranbat = new Random();

			if (Image == null) throw new InvalidOperationException("Set Image property first.");
            colors = new Color[12];
            for (int d = 0; d < colors.Length; d++)
            {
                colors[d] = Color.FromArgb(ranbat.Next(256), ranbat.Next(256), ranbat.Next(256));
                
            }

            using (Graphics g = Graphics.FromImage(Overlay))
            {
                String successStr = "";
                SimplePixel[,] allPoints = new SimplePixel[Overlay.Width + 1, Overlay.Height + 1];
                for (int i = 0; i < Overlay.Width; i++)
                {
                    for (int j = 0; j < Overlay.Height; j++)
                    {
                        allPoints[i, j] = new SimplePixel(i, j);
                    }
                }

                //draw all selected points
                for (int p = 0; p < points.Count; p++)
                {
                    Point temp = points[p];
                    g.DrawEllipse(pen, temp.X, temp.Y, 5, 5);
                }
                Program.MainForm.RefreshImage();

                for (int i = 0; i < points.Count; i++)
                {
                    Point start = points[i];
                    SimplePixel startP = allPoints[start.X, start.Y];
   //                 g.DrawEllipse(pen, start.X, start.Y, 5, 5);
                    Point end = points[(i + 1) % points.Count];
                    SimplePixel endP = allPoints[end.X, end.Y];
                    SimplePixel curP = startP;

                    while (curP != endP)
                    {
                        SimplePixel min = new SimplePixel(0,0);
                        int minC = 999999;

                        SimplePixel up = allPoints[curP.X, curP.Y - 1]; ;
                        if ((!up.Visited) && (up.Y > 0))
                        {
                            int upC = GetPixelWeight(new Point(up.X, up.Y));
                            min = up;
                            minC = upC;
                        }

                        SimplePixel right = allPoints[curP.X + 1, curP.Y];
                        if ((!right.Visited) && (up.X < Overlay.Width - 1))
                        {
                            int rightC = GetPixelWeight(new Point(right.X, right.Y));
                            if (rightC < minC)
                            {
                                min = right;
                                minC = rightC;
                            }
                        }

                        SimplePixel left = allPoints[curP.X - 1, curP.Y];
                        if ((!left.Visited) && (up.X > 0))
                        {
                            int leftC = GetPixelWeight(new Point(left.X, left.Y));
                            if (leftC < minC)
                            {
                                min = left;
                                minC = leftC;
                            }
                        }

                        SimplePixel down = allPoints[curP.X, curP.Y + 1];
                        if ((!down.Visited) && (up.Y < Overlay.Height - 1))
                        {
                            int downC = GetPixelWeight(new Point(down.X, down.Y));
                            if (downC < minC)
                            {
                                min = down;
                                minC = downC;
                            }
                        }

                        curP = min;
                        if ((curP.X != 0) && (curP.Y != 0))
                        {
                            
                            Overlay.SetPixel(curP.X, curP.Y, (Color)colors[i % colors.Length]);
                            Program.MainForm.RefreshImage();
                            curP.Visited = true;
                        }
                        else
                        {
                            //g.DrawRectangle(blackpen, new Rectangle(0, 0, 15, 5));
                            //g.DrawString("Dead end - Cannot continue", font, brush, new PointF(0F, 0F));
                            //reset visited values before starting new search from next point
                            for (int row = 0; row < Overlay.Width; row++)
                            {
                                for (int col = 0; col < Overlay.Height; col++)
                                {
                                    allPoints[row, col].Visited = false;
                                }
                            }
                            break;
                        }

                    }

                    if ((curP.X == endP.X) && (curP.Y == curP.Y))
                    {
                        successStr += "Success for (" + startP.X + "," + startP.Y + ") to (" + endP.X + "," + endP.Y + ")\n";
                    }
                    //Program.MainForm.RefreshImage();
                }

                //The algorithm succeeds in six paths for these points
                //(151, 49); (235, 29);; (334, 68); (376, 182); (377, 253); (370, 293); (324, 320); (317, 345); (300, 444); (222, 424); (156, 360); (131, 274); (120, 172)
                g.DrawString(successStr, font, brush, new PointF(0F, 0F));
                Program.MainForm.RefreshImage();
            }
            
		}
	}
}
