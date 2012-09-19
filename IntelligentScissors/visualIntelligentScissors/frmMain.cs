using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace VisualIntelligentScissors
{
	public partial class frmMain : Form
	{
		private GrayBitmap image = new GrayBitmap();
		private StopWatch timer = new StopWatch();
		public static readonly string SegmentPointRegex = @"\((\d+), *(\d+)\)";

		public frmMain()
		{
			InitializeComponent();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			btnOpenFile_Click(this, null);
		}

		private void btnSaveFile_Click(object sender, EventArgs e)
		{
			if (image.Bitmap == null)
			{
				MessageBox.Show("No image loaded to save.");
				return;
			}
			if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
			using (Stream s = saveFileDialog1.OpenFile())
				image.Bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Png);
		}

		/// <summary>
		/// Gets/sets the status text to appear in the toolstrip at the 
		/// bottom (status bar).
		/// </summary>
		public string Status
		{
			get { return lblStatus.Text == "Idle" ? null : lblStatus.Text; }
			set { lblStatus.Text = value == null ? "Idle" : value; }
		}

		public void RefreshImage()
		{
			boxImage.Refresh();
			boxGradient.Refresh();
		}

		private void BackgroundProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		#region File opening operation
		private void btnOpenFile_Click(object sender, EventArgs e)
		{
			DialogResult result = openFileDialog1.ShowDialog();
			// see if user selected a file. 
			if (result != DialogResult.OK) return;
			UseWaitCursor = true;
			Status = "Loading image";
			Application.DoEvents(); // Allow time for wait cursor to turn on
			try
			{
				// clear the segmentation points box. 
				tbSegmentPoints.Text = "Click image to add segmentation points";

				// Disable opening another file until this one is done.
				btnOpenFile.Enabled = false;
				btnScissors.Enabled = false;

				// Load the data file.
				FileLoader.RunWorkerAsync(new FileLoadingParams(openFileDialog1.OpenFile()));
			}
			finally
			{
				UseWaitCursor = false;
			}
		}

		struct FileLoadingParams
		{
			public FileLoadingParams(Stream fileStream)
			{
				FileStream = fileStream;
			}
			public readonly Stream FileStream;
		}

		struct FileLoadingResult
		{
			public FileLoadingResult(Exception ex)
			{
				this.Exception = ex;
				this.SegmentationPoints = null;
				this.GrayImage = null;
			}
			public FileLoadingResult(GrayBitmap grayImage, string segmentationPoints)
			{
				this.GrayImage = grayImage;
				this.SegmentationPoints = segmentationPoints;
				this.Exception = null;
			}
			public readonly GrayBitmap GrayImage;
			public readonly string SegmentationPoints;
			public readonly Exception Exception;
		}

		private void FileLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			FileLoadingParams fileLoadingParams = (FileLoadingParams)e.Argument;

			timer.Start();
			GrayBitmap img = new GrayBitmap();
			string segmentationPoints;
			using (Stream file = fileLoadingParams.FileStream)
				img.Load(file, out segmentationPoints, new ProgressCallback(FileLoader.ReportProgress));
			timer.Stop();

			e.Result = new FileLoadingResult(img, segmentationPoints);
		}

		private void FileLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			FileLoadingResult result = (FileLoadingResult)e.Result;
			Status = null;
			lblLastOperationTime.Text = timer.Elapsed.ToString();

			if (result.GrayImage != null)
			{
				image = result.GrayImage;
				tbSegmentPoints.Text = result.SegmentationPoints;
				// Display the image. 
				boxImage.BackgroundImage = image.Bitmap;
				boxImage.Image = PrepareOverlay();
				boxGradient.BackgroundImage = image.Gradient.Bitmap;
				boxGradient.Image = boxImage.Image;

				// Enable toolbar buttons
				//btnShowGradient.Enabled = true;
				btnSaveFile.Enabled = true;

				// Size the application window appropriately.
				Width += boxImage.BackgroundImage.Width - boxImage.Width;
				Height += boxImage.BackgroundImage.Height - boxImage.Height;

				// Start generating the gradient right away
				GenerateGradient();

				tabControl1.SelectTab(tabImage);
			}
			else if (result.Exception != null)
			{
				if (MessageBox.Show(this, result.Exception.ToString(), "Error during image load.",
					MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
					GenerateGradient();
			}
		}

		#endregion

		#region Gradient generating mechanism
		private void GenerateGradient()
		{
			if (GradientGenerator.IsBusy) return;
			Status = "Generating gradient image";
			GradientGenerator.RunWorkerAsync(image.Clone());
		}

		private void GradientGenerator_DoWork(object sender, DoWorkEventArgs e)
		{
			GrayBitmap asyncImage = (GrayBitmap)e.Argument;
			try
			{
				if (asyncImage.Gradient.Bitmap == null)
				{
					// Don't step through this code with an Locals or Auto 
					// window opened at all. VS will crash the background thread.
					timer.Start();
					asyncImage.Gradient.GenerateGradientBitmap(new ProgressCallback(GradientGenerator.ReportProgress));
					timer.Stop();
					e.Result = asyncImage.Gradient;
				}
			}
			catch (Exception ex)
			{
				e.Result = ex;
			}
			finally
			{
				asyncImage.Bitmap.Dispose();
			}
		}

		private void GradientGenerator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			btnOpenFile.Enabled = true; // re-enable loading files

			Status = null;
			lblLastOperationTime.Text = timer.Elapsed.ToString();
			if (e.Result is GradientBitmap)
			{
				image.Gradient.CopyFrom((GradientBitmap)e.Result);
				boxGradient.BackgroundImage = image.Gradient.Bitmap;
				boxGradient.Refresh();
				btnScissors.Enabled = true;
			}
			else if (e.Result is Exception)
			{
				if (MessageBox.Show(this, e.Result.ToString(), "Error during gradient generation",
					MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
					GenerateGradient();
			}
		}

		#endregion

		#region Segmentation point handling
		private Bitmap overlay;
		private Image PrepareOverlay()
		{
			return overlay = new Bitmap(boxImage.BackgroundImage.Width, boxImage.BackgroundImage.Height);
		}

		private void imgBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (image.Bitmap == null) return; // no image loaded to operate against.
			// write the current mouse location into the image 
			// segmation point list text box.  This can be cut and 
			// pasted into a comment in a pgm file. 
			if (!tbSegmentPoints.Text.Contains("("))
				tbSegmentPoints.Text = ("(" + e.X + ", " + e.Y + ")");
			else
				tbSegmentPoints.AppendText("; (" + e.X + ", " + e.Y + ")");
			overlay.SetPixel(e.X, e.Y, Color.White);
			RefreshImage();
		}

		private IList<Point> GetPoints()
		{
			MatchCollection mc = Regex.Matches(tbSegmentPoints.Text, SegmentPointRegex);
			List<Point> list = new List<Point>(mc.Count);
			foreach (Match m in mc)
				list.Add(new Point(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)));
			return list;
		}

		private void btnCopyPointsToClipboard_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(tbSegmentPoints.Text);
		}

		private void btnClearPoints_Click(object sender, EventArgs e)
		{
			tbSegmentPoints.Clear();
		}

		#endregion

		#region Scissor operations
		private void UseScissors(Scissors scissors, Pen pen)
		{
			if (scissors == null) throw new ArgumentNullException("scissors");
			if (image.Gradient.Bitmap == null) throw new InvalidOperationException("Cannot use scissors until gradient is calculated.");
			UseWaitCursor = true;
			Application.DoEvents(); // Allow time for wait cursor to turn on
		tryagain:
			try
			{
				scissors.Image = image;
				scissors.Overlay = overlay;
				Status = "Tracing";
				timer.Start();
				scissors.FindSegmentation(GetPoints(), pen);
				timer.Stop();
				Status = null;
				lblLastOperationTime.Text = timer.Elapsed.ToString();
				Refresh();
			}
			catch (Exception ex)
			{
				Status = "Tracing error";
				if (MessageBox.Show(this, ex.ToString(), "Error during scissor operation",
					MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
					goto tryagain;
			}
			finally
			{
				UseWaitCursor = false;
			}
		}

		private void btnStraightScissors_Click(object sender, EventArgs e)
		{
			UseScissors(new StraightScissors(), Pens.Red);
		}

		private void btnSimpleScissors_Click(object sender, EventArgs e)
		{
			UseScissors(new SimpleScissors(), Pens.Orange);
		}

		private void btnIntelligentScissors_Click(object sender, EventArgs e)
		{
			UseScissors(new DijkstraScissors(), Pens.Yellow);
		}

		private void btnScissors_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			btnScissors.Text = e.ClickedItem.Text;
			btnScissors.Tag = e.ClickedItem;
		}

		private void btnScissors_ButtonClick(object sender, EventArgs e)
		{
			if (btnScissors.Tag == btnStraightScissors)
				btnStraightScissors_Click(sender, e);
			else if (btnScissors.Tag == btnSimpleScissors)
				btnSimpleScissors_Click(sender, e);
			else if (btnScissors.Tag == btnIntelligentScissors)
				btnIntelligentScissors_Click(sender, e);
			else
				btnScissors.ShowDropDown();
		}

		#endregion

		private void btnClearOverlay_Click(object sender, EventArgs e)
		{
			boxImage.Image = boxGradient.Image = PrepareOverlay();
		}
	}
}