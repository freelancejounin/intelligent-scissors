using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace VisualIntelligentScissors
{
	public class GradientBitmap
	{
		/// <summary>
		/// Instantiates a <see cref="GradientBitmap"/> class.
		/// </summary>
		public GradientBitmap(GrayBitmap original)
		{
			if (original == null) throw new ArgumentNullException("original");
			this.grayBitmap = original;
			Reset();
		}

		private readonly GrayBitmap grayBitmap;
		public GrayBitmap GrayBitmap
		{
			get { return grayBitmap; }
		}

		DirectBitmapReader directBitmapReader;

		private short maximumGradientValue;
		public short MaximumGradientValue
		{
			get { return maximumGradientValue; }
            set { maximumGradientValue = value; }
		}

		private Bitmap bitmap;
		public Bitmap Bitmap
		{
			get { return bitmap; }
			set { bitmap = value; }
		}

		/// <summary>
		/// Calculate the gradient of a given pixel.
		/// </summary>
		/// <param name="point">The pixel to query for a gradient.</param>	
		/// <returns>The gradient of the given pixel, considering x and y directions.</returns>
		public byte GetPixelGradient(Point point)
		{
			return GetPixelGradient(point.X, point.Y);
		}

		/// <summary>
		/// Calculate the gradient of a given pixel.
		/// </summary>
		/// <param name="x">x coordinate of pixel, must be between 1..cols-1</param>
		/// <param name="y">y coordinate of pixel, must be between 1..rows-1</param>
		/// <returns>The gradient of the given pixel, considering x and y directions.</returns>
		public byte GetPixelGradient(int x, int y)
		{
			int GXxy = 0;//the y portion
			int GYxy = 0;//the x portion
			byte Gxy = 0;//final value

			if (directBitmapReader == null)
				directBitmapReader = new DirectBitmapReader(grayBitmap.Bitmap);

			GXxy = ((directBitmapReader.GetGrayPixel(x + 1, y + 1)) - (directBitmapReader.GetGrayPixel(x - 1, y + 1)));
			GXxy = GXxy + (2 * ((directBitmapReader.GetGrayPixel(x + 1, y)) - (directBitmapReader.GetGrayPixel(x - 1, y))));
			GXxy = GXxy + ((directBitmapReader.GetGrayPixel(x + 1, y - 1)) - (directBitmapReader.GetGrayPixel(x - 1, y - 1)));
			GXxy = GXxy / 4;

			GYxy = ((directBitmapReader.GetGrayPixel(x + 1, y + 1)) - (directBitmapReader.GetGrayPixel(x + 1, y - 1)));
			GYxy = GYxy + (2 * ((directBitmapReader.GetGrayPixel(x, y + 1)) - (directBitmapReader.GetGrayPixel(x, y - 1))));
			GYxy = GYxy + ((directBitmapReader.GetGrayPixel(x - 1, y + 1)) - (directBitmapReader.GetGrayPixel(x - 1, y - 1)));
			GYxy = GYxy / 4;

			/* This is how we did this in CS 450, it's faster than doing
			 a square root, but not quite as accurate, hence the clamping.
			 It does bring out more edges, but I don't know if that is 
			 good or not - Ryan
			Gxy = (Math.Abs(GXxy) + Math.Abs(GYxy));
			Gxy = Math.Max(0, Gxy);
			Gxy = Math.Min(imgMaxGray, Gxy);
			*/

			Gxy = (byte)Math.Sqrt((double)(GXxy * GXxy) + (double)(GYxy * GYxy));
			return Gxy;
		}

		/// <summary>
		/// Calculates the gradient for each pixel in this image then returns a bitmap 
		/// in which the color corresponds to the gradient.
		/// </summary>
		/// <returns></returns>
		public virtual void GenerateGradientBitmap(ProgressCallback callback)
		{
			Reset();
			directBitmapReader = new DirectBitmapReader(grayBitmap.Bitmap);
			bitmap = new Bitmap(grayBitmap.Bitmap.Width, grayBitmap.Bitmap.Height);
			using (DirectBitmapWriter dbw = new DirectBitmapWriter(bitmap))
			{

				for (int i = 1; i < Bitmap.Height - 1; i++)
				{
					if (callback != null) callback(i * 100 / (Bitmap.Height - 2));
					for (int j = 1; j < Bitmap.Width - 1; j++)
					{
						byte grad = GetPixelGradient(j, i);
						dbw.SetGrayPixel(j, i, grad);
						if (grad > maximumGradientValue) maximumGradientValue = grad;
					}
				}
			}
		}

		public virtual void Reset()
		{
			maximumGradientValue = -1;
			if (bitmap != null)
			{
				bitmap.Dispose();
				bitmap = null;
			}
			directBitmapReader = null;
		}

		/// <summary>
		/// Clones this object so it can be safely read from another thread.
		/// </summary>
		/// <remarks>
		/// The clone will point to the original gray image.
		/// </remarks>
		public virtual GradientBitmap Clone(GrayBitmap newClonedGrayImage)
		{
			GradientBitmap clone = new GradientBitmap(newClonedGrayImage);
			clone.maximumGradientValue = this.maximumGradientValue;
			if (this.bitmap != null)
				clone.bitmap = (Bitmap)this.bitmap.Clone();
			return clone;
		}

		/// <summary>
		/// Clones all attributes from another GradientBitmap into this existing one.
		/// </summary>
		public virtual void CopyFrom(GradientBitmap model)
		{
			this.maximumGradientValue = model.maximumGradientValue;
			this.bitmap = (Bitmap)model.bitmap.Clone();
		}
	}
}
