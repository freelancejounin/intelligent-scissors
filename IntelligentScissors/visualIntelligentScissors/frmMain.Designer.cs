namespace VisualIntelligentScissors
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.tbSegmentPoints = new System.Windows.Forms.TextBox();
			this.btnClearPoints = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
			this.btnSaveFile = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnScissors = new System.Windows.Forms.ToolStripSplitButton();
			this.btnStraightScissors = new System.Windows.Forms.ToolStripMenuItem();
			this.btnSimpleScissors = new System.Windows.Forms.ToolStripMenuItem();
			this.btnIntelligentScissors = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabImage = new System.Windows.Forms.TabPage();
			this.boxImage = new System.Windows.Forms.PictureBox();
			this.tabGradient = new System.Windows.Forms.TabPage();
			this.boxGradient = new System.Windows.Forms.PictureBox();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.btnCopyPointsToClipboard = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.lblLastOperationTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.FileLoader = new System.ComponentModel.BackgroundWorker();
			this.GradientGenerator = new System.ComponentModel.BackgroundWorker();
			this.btnClearOverlay = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.boxImage)).BeginInit();
			this.tabGradient.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.boxGradient)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "pgm images|*.pgm|All files|*.*";
			// 
			// tbSegmentPoints
			// 
			this.tbSegmentPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbSegmentPoints.Location = new System.Drawing.Point(12, 373);
			this.tbSegmentPoints.Name = "tbSegmentPoints";
			this.tbSegmentPoints.Size = new System.Drawing.Size(337, 20);
			this.tbSegmentPoints.TabIndex = 3;
			this.tbSegmentPoints.Text = "click on image to add segmentation points";
			// 
			// btnClearPoints
			// 
			this.btnClearPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearPoints.Location = new System.Drawing.Point(384, 373);
			this.btnClearPoints.Name = "btnClearPoints";
			this.btnClearPoints.Size = new System.Drawing.Size(78, 20);
			this.btnClearPoints.TabIndex = 5;
			this.btnClearPoints.Text = "Clear Points";
			this.btnClearPoints.UseVisualStyleBackColor = true;
			this.btnClearPoints.Click += new System.EventHandler(this.btnClearPoints_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenFile,
            this.btnSaveFile,
            this.toolStripSeparator1,
            this.btnScissors,
            this.btnClearOverlay});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(476, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
			this.btnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(53, 22);
			this.btnOpenFile.Text = "Open";
			this.btnOpenFile.ToolTipText = "Open PGM image";
			this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// btnSaveFile
			// 
			this.btnSaveFile.Enabled = false;
			this.btnSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFile.Image")));
			this.btnSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSaveFile.Name = "btnSaveFile";
			this.btnSaveFile.Size = new System.Drawing.Size(51, 22);
			this.btnSaveFile.Text = "Save";
			this.btnSaveFile.ToolTipText = "Save PNG image";
			this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnScissors
			// 
			this.btnScissors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStraightScissors,
            this.btnSimpleScissors,
            this.btnIntelligentScissors});
			this.btnScissors.Enabled = false;
			this.btnScissors.Image = ((System.Drawing.Image)(resources.GetObject("btnScissors.Image")));
			this.btnScissors.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnScissors.Name = "btnScissors";
			this.btnScissors.Size = new System.Drawing.Size(77, 22);
			this.btnScissors.Text = "Scissors";
			this.btnScissors.ButtonClick += new System.EventHandler(this.btnScissors_ButtonClick);
			this.btnScissors.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.btnScissors_DropDownItemClicked);
			// 
			// btnStraightScissors
			// 
			this.btnStraightScissors.Name = "btnStraightScissors";
			this.btnStraightScissors.Size = new System.Drawing.Size(173, 22);
			this.btnStraightScissors.Text = "Straight scissors";
			this.btnStraightScissors.Click += new System.EventHandler(this.btnStraightScissors_Click);
			// 
			// btnSimpleScissors
			// 
			this.btnSimpleScissors.Name = "btnSimpleScissors";
			this.btnSimpleScissors.Size = new System.Drawing.Size(173, 22);
			this.btnSimpleScissors.Text = "Simple scissors";
			this.btnSimpleScissors.Click += new System.EventHandler(this.btnSimpleScissors_Click);
			// 
			// btnIntelligentScissors
			// 
			this.btnIntelligentScissors.Name = "btnIntelligentScissors";
			this.btnIntelligentScissors.Size = new System.Drawing.Size(173, 22);
			this.btnIntelligentScissors.Text = "Intelligent scissors";
			this.btnIntelligentScissors.Click += new System.EventHandler(this.btnIntelligentScissors_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabImage);
			this.tabControl1.Controls.Add(this.tabGradient);
			this.tabControl1.Location = new System.Drawing.Point(12, 28);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(450, 337);
			this.tabControl1.TabIndex = 2;
			// 
			// tabImage
			// 
			this.tabImage.Controls.Add(this.boxImage);
			this.tabImage.Location = new System.Drawing.Point(4, 22);
			this.tabImage.Name = "tabImage";
			this.tabImage.Padding = new System.Windows.Forms.Padding(3);
			this.tabImage.Size = new System.Drawing.Size(442, 311);
			this.tabImage.TabIndex = 0;
			this.tabImage.Text = "Image";
			this.tabImage.UseVisualStyleBackColor = true;
			// 
			// boxImage
			// 
			this.boxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.boxImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.boxImage.Location = new System.Drawing.Point(3, 3);
			this.boxImage.Name = "boxImage";
			this.boxImage.Size = new System.Drawing.Size(436, 305);
			this.boxImage.TabIndex = 4;
			this.boxImage.TabStop = false;
			this.boxImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgBox_MouseClick);
			// 
			// tabGradient
			// 
			this.tabGradient.Controls.Add(this.boxGradient);
			this.tabGradient.Location = new System.Drawing.Point(4, 22);
			this.tabGradient.Name = "tabGradient";
			this.tabGradient.Padding = new System.Windows.Forms.Padding(3);
			this.tabGradient.Size = new System.Drawing.Size(442, 311);
			this.tabGradient.TabIndex = 1;
			this.tabGradient.Text = "Gradient";
			this.tabGradient.UseVisualStyleBackColor = true;
			// 
			// boxGradient
			// 
			this.boxGradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.boxGradient.Dock = System.Windows.Forms.DockStyle.Fill;
			this.boxGradient.Location = new System.Drawing.Point(3, 3);
			this.boxGradient.Name = "boxGradient";
			this.boxGradient.Size = new System.Drawing.Size(436, 305);
			this.boxGradient.TabIndex = 7;
			this.boxGradient.TabStop = false;
			this.boxGradient.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgBox_MouseClick);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "png";
			this.saveFileDialog1.Filter = "PNG Images|*.png|All files|*.*";
			// 
			// btnCopyPointsToClipboard
			// 
			this.btnCopyPointsToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopyPointsToClipboard.AutoSize = true;
			this.btnCopyPointsToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyPointsToClipboard.Image")));
			this.btnCopyPointsToClipboard.Location = new System.Drawing.Point(355, 371);
			this.btnCopyPointsToClipboard.Name = "btnCopyPointsToClipboard";
			this.btnCopyPointsToClipboard.Size = new System.Drawing.Size(23, 22);
			this.btnCopyPointsToClipboard.TabIndex = 4;
			this.btnCopyPointsToClipboard.UseVisualStyleBackColor = true;
			this.btnCopyPointsToClipboard.Click += new System.EventHandler(this.btnCopyPointsToClipboard_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.AllowItemReorder = true;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar,
            this.lblLastOperationTime});
			this.statusStrip1.Location = new System.Drawing.Point(0, 396);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(476, 22);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(25, 17);
			this.lblStatus.Text = "Idle";
			// 
			// progressBar
			// 
			this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(200, 16);
			// 
			// lblLastOperationTime
			// 
			this.lblLastOperationTime.Name = "lblLastOperationTime";
			this.lblLastOperationTime.Size = new System.Drawing.Size(29, 17);
			this.lblLastOperationTime.Text = "0:00";
			// 
			// FileLoader
			// 
			this.FileLoader.WorkerReportsProgress = true;
			this.FileLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FileLoader_DoWork);
			this.FileLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FileLoader_RunWorkerCompleted);
			this.FileLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundProcess_ProgressChanged);
			// 
			// GradientGenerator
			// 
			this.GradientGenerator.WorkerReportsProgress = true;
			this.GradientGenerator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GradientGenerator_DoWork);
			this.GradientGenerator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GradientGenerator_RunWorkerCompleted);
			this.GradientGenerator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundProcess_ProgressChanged);
			// 
			// btnClearOverlay
			// 
			this.btnClearOverlay.Image = ((System.Drawing.Image)(resources.GetObject("btnClearOverlay.Image")));
			this.btnClearOverlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnClearOverlay.Name = "btnClearOverlay";
			this.btnClearOverlay.Size = new System.Drawing.Size(91, 22);
			this.btnClearOverlay.Text = "Clear overlay";
			this.btnClearOverlay.Click += new System.EventHandler(this.btnClearOverlay_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(476, 418);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnCopyPointsToClipboard);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.btnClearPoints);
			this.Controls.Add(this.tbSegmentPoints);
			this.MinimumSize = new System.Drawing.Size(400, 201);
			this.Name = "frmMain";
			this.Text = "CS 312 Intelligent Scissors";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabImage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.boxImage)).EndInit();
			this.tabGradient.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.boxGradient)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnClearPoints;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnOpenFile;
		private System.Windows.Forms.ToolStripButton btnSaveFile;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabImage;
		private System.Windows.Forms.TabPage tabGradient;
		private System.Windows.Forms.PictureBox boxImage;
		private System.Windows.Forms.PictureBox boxGradient;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripSplitButton btnScissors;
		private System.Windows.Forms.ToolStripMenuItem btnStraightScissors;
		private System.Windows.Forms.ToolStripMenuItem btnSimpleScissors;
		private System.Windows.Forms.ToolStripMenuItem btnIntelligentScissors;
		private System.Windows.Forms.Button btnCopyPointsToClipboard;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.ToolStripStatusLabel lblLastOperationTime;
		public System.Windows.Forms.TextBox tbSegmentPoints;
		private System.ComponentModel.BackgroundWorker FileLoader;
		private System.ComponentModel.BackgroundWorker GradientGenerator;
		private System.Windows.Forms.ToolStripButton btnClearOverlay;
    }
}

