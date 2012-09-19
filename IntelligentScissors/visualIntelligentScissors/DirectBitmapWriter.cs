using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace VisualIntelligentScissors
{
	internal class DirectBitmapWriter : IDisposable
	{
		private Bitmap bitmap;
		private byte[] imageBuffer = null;
		private BitmapData bitmapData = null;

		private const int pixelWidth = 3;

		public DirectBitmapWriter(Bitmap bitmap)
		{
			this.bitmap = bitmap;

			try
			{
				InitializeBuffer();
			}
			catch (SecurityException) { } // we may not have SecurityPermission for UnmanagedCode.
		}

		/// <summary>
		/// Initializes the bitmap data and imagebuffer.
		/// </summary>
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
		private void InitializeBuffer()
		{
			bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

			imageBuffer = new byte[bitmapData.Stride * bitmap.Height];
		}

		/// <summary>
		/// Sets the pixel in the image buffer.
		/// </summary>
		/// <param name="x">X-Coordinate of the pixel</param>
		/// <param name="y">Y-Coordinate of the pixel</param>
		/// <param name="color">The color to be assigned to blue, green, and red.</param>
		public void SetGrayPixel(int x, int y, byte color)
		{
			if (imageBuffer != null)
			{
				// optimized mode
				imageBuffer[bitmapData.Stride * y + x * pixelWidth] = color;
				imageBuffer[bitmapData.Stride * y + x * pixelWidth + sizeof(byte)] = color;
				imageBuffer[bitmapData.Stride * y + x * pixelWidth + 2 * sizeof(byte)] = color;
			}
			else
			{
				// managed mode
				bitmap.SetPixel(x, y, Color.FromArgb(color, color, color));
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (imageBuffer != null)
				releaseArray();
		}

		/// <summary>
		/// Copy the image buffer back to bitmapdata, then copy it to the original bitmap (image).
		/// </summary>
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		private void releaseArray()
		{
			Marshal.Copy(imageBuffer, 0, bitmapData.Scan0, bitmapData.Stride * bitmap.Height);
			bitmap.UnlockBits(bitmapData);
			bitmapData = null;
			imageBuffer = null;
		}

		#endregion
	}

}
