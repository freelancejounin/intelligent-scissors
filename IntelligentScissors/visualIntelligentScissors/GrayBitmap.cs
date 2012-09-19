using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace VisualIntelligentScissors
{
	public delegate void ProgressCallback(int percent);

	public class GrayBitmap
	{
		public GrayBitmap()
		{
			gradient = new GradientBitmap(this);
		}

		public GrayBitmap Clone()
		{
			GrayBitmap clone = new GrayBitmap();
			if (bitmap != null)
				clone.bitmap = (Bitmap)bitmap.Clone();
			if (gradient != null)
				clone.gradient = gradient.Clone(clone);
			clone.maximumGrayValue = maximumGrayValue;
			return clone;
		}

		private Bitmap bitmap;
		public Bitmap Bitmap
		{
			get { return bitmap; }
		}

		private GradientBitmap gradient;

		public GradientBitmap Gradient
		{
			get { return gradient; }
		}

		private int maximumGrayValue;

		#region Loading support
		/// <summary>
		/// place holder for image loader which comes in text and binary flavors. 
		/// </summary>
		/// <param name="filename">name of file containing image</param>
		/// <returns></returns>
		public void Load(Stream file, out string segmentationPoints, ProgressCallback callback)
		{
			Gradient.Reset();
			loadBinary(file, out segmentationPoints, callback);
		}

		private int parseANumber(BinaryReader b)
		{
			StringBuilder number = new StringBuilder(byte.MaxValue.ToString().Length);
			number.Append(b.ReadChar());
			// spin through white space prefix
			while (number[0] == ' ' || number[0] == '\n')
				number[0] = b.ReadChar();
			// get some digits.  ASSUME nothing but digits and white space
			char ch;
			while ((ch = b.ReadChar()) != ' ' && ch != '\n')
				number.Append(ch);
			return Int32.Parse(number.ToString());
		}

		/// <summary>
		///  load a binary pgm file.  
		/// </summary>
		/// <param name="filename">file containing the image to load</param>
		/// <returns>string</returns>
		private void loadBinary(Stream file, out string segmentationPoints, ProgressCallback callback)
		{
			// Create the reader for data.
			using (BinaryReader r = new BinaryReader(file))
			{
				// read in a line. 
				char[] chars = new char[255];
				chars = r.ReadChars(2);
				if (chars[1] != '5')
				{
					file.Position = 0; // reset to starting position of stream.
					loadAscii(file, out segmentationPoints, callback);
					return;
				}
				//spin through the comments. 
				while (chars[0] != '\n')
					chars[0] = r.ReadChar();
				chars[0] = (char)r.PeekChar();
				while (chars[0] == '#')
				{
					while (chars[0] != '\n')
						chars[0] = r.ReadChar();
					chars[0] = (char)r.PeekChar();
				}
				segmentationPoints = string.Empty; // TODO: read segment points from binary file

				// next up:  width height and max gray value. 
				int imgCols = parseANumber(r);
				int imgRows = parseANumber(r);
				maximumGrayValue = parseANumber(r);

				// now read in the image all at once (can do this with binary images). 
				bitmap = new Bitmap(imgCols, imgRows);
				byte[] imageRaw = new byte[imgRows * imgCols];
				imageRaw = r.ReadBytes(imgRows * imgCols);
				int rr = 0;
				while (rr < imgRows)
				{
					callback(rr * 100 / imgRows);
					int cc = 0;
					while (cc < imgCols)
					{
						byte pixelValue = imageRaw[(imgCols * rr) + cc];
						bitmap.SetPixel(cc, rr, Color.FromArgb(pixelValue, pixelValue, pixelValue));
						cc++;
					}
					rr++;
				}
			}
		}

		/// <summary>
		///  ascii version of the image loader. works fine. 
		/// but is dog-slow (when debugging only).  Would go faster if it read the whole file in at once instead
		/// of line by line? 
		/// </summary>
		/// <param name="filename">file containing the image to load.</param>
		/// <returns>string</returns>
		private void loadAscii(Stream file, out string segmentationPoints, ProgressCallback callback)
		{
			using (StreamReader sr = new StreamReader(file))
			{
				// grok out the magic number etc, which are in plain text. 
				String line = sr.ReadLine();
				if (line != "P2")
					throw new ApplicationException("unsupported PGM filetype, try type P2 or P5 only.");

				// spin through comments if any. 
				segmentationPoints = string.Empty;
				while ((line = sr.ReadLine()).StartsWith("#"))
					if (Regex.IsMatch(line, frmMain.SegmentPointRegex))
						segmentationPoints = line.Substring(line.IndexOf("("));

				// parse out the width and height. 
				string[] pieces;
				pieces = line.Split(' ');
				int imgCols = Int32.Parse(pieces[0]);
				int imgRows = Int32.Parse(pieces[1]);

				// Initialize the image with the size just read in.
				bitmap = new Bitmap(imgCols, imgRows);
				using (DirectBitmapWriter dba = new DirectBitmapWriter(bitmap))
				{

					// get the max gray value. 
					maximumGrayValue = Int32.Parse(sr.ReadLine());

					// Prepare to receive pixels: first row, first column
					int pixelRow = 0, pixelColumn = 0;
					while (pixelRow < imgRows)
					{
						// Each line will contain only a few of the pixels on a single row of the image.
						line = sr.ReadLine();
						string[] colEntries = line.Split(' ');
						for (int ii = 0; ii < colEntries.Length; ii++)
						{
							if (colEntries[ii].Length == 0) continue; // skip "  " double spaces
							byte thisColor = byte.Parse(colEntries[ii]);
							dba.SetGrayPixel(pixelColumn, pixelRow, thisColor);
							if (++pixelColumn == imgCols)
							{
								pixelColumn = 0;
								pixelRow++;
								callback(pixelRow * 100 / imgRows);
							}
						}
					}
				}
			}
		}

		#endregion
	}
}
