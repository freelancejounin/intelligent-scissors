using System;
using System.Collections.Generic;
using System.Drawing;

namespace VisualIntelligentScissors
{
	public abstract class Scissors
	{
		protected Scissors() { }
		protected Scissors(GrayBitmap image, Bitmap overlay)
		{
			Image = image;
			Overlay = overlay;
		}

		private GrayBitmap image;
		/// <summary>
		/// The image to perform the scissor operation on.
		/// </summary>
		public GrayBitmap Image
		{
			get { return image; }
			set { image = value; }
		}

		private Bitmap overlay;

		public Bitmap Overlay
		{
			get { return overlay; }
			set { overlay = value; }
		}


		/// <summary>
		/// Gets the weight of a given pixel.
		/// </summary>
		protected int GetPixelWeight(Point point)
		{
			if (Image == null) throw new InvalidOperationException("Set Image property first.");
			return 1 + Image.Gradient.MaximumGradientValue - Image.Gradient.GetPixelGradient(point);
		}

		/// <summary>
		/// Draws a line along the points given, closing the loop
		/// by drawing a line between the last and first points.
		/// </summary>
		/// <param name="points">The list of points to trace.</param>
		/// <param name="pen">The pen to use in drawing the line.</param>
		public abstract void FindSegmentation(IList<Point> points, Pen pen);

	}
}
