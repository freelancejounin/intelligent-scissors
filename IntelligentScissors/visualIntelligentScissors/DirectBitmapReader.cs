using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace VisualIntelligentScissors
{
	internal class DirectBitmapReader
	{
		private Bitmap bitmap;
		private byte[] imageBuffer = null;
		private int stride;
		private const int pixelWidth = 3;

		public DirectBitmapReader(Bitmap bitmap)
		{
			this.bitmap = bitmap;

			try
			{
				InitializeBuffer(bitmap);
			}
			catch (SecurityException) { } // we may not have SecurityPermission for UnmanagedCode.
		}

		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		private void InitializeBuffer(Bitmap bitmap)
		{
			BitmapData bitmapData;
			bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			stride = bitmapData.Stride;

			imageBuffer = new byte[bitmapData.Stride * bitmap.Height];
			Marshal.Copy(bitmapData.Scan0, imageBuffer, 0, bitmapData.Stride * bitmap.Height);
			bitmap.UnlockBits(bitmapData);
		}

		public byte GetGrayPixel(int x, int y)
		{
			if (imageBuffer != null)
				return imageBuffer[stride * y + x * pixelWidth]; // optimized
			else
				return bitmap.GetPixel(x, y).G; // managed
		}
	}
}
