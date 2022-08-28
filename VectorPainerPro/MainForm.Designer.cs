

namespace VectorPainerPro
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripTools = new System.Windows.Forms.ToolStrip();
            this.btnLoadTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewFile = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnPencil = new System.Windows.Forms.ToolStripButton();
            this.openDllDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRepeat = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.labelMainColor = new System.Windows.Forms.Label();
            this.labelFillColor = new System.Windows.Forms.Label();
            this.btnFillColor = new System.Windows.Forms.Button();
            this.btnMainColor = new System.Windows.Forms.Button();
            this.labelLineThickness = new System.Windows.Forms.Label();
            this.trackBarLineThickness = new System.Windows.Forms.TrackBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panelPictureBox = new System.Windows.Forms.Panel();
            this.buttonCreateAnimation = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelCanvaSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.button_Load = new System.Windows.Forms.Button();
            this.toolStripTools.SuspendLayout();
            this.panelSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panelPictureBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripTools
            // 
            this.toolStripTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadTool,
            this.toolStripSeparator1,
            this.btnNewFile,
            this.btnSave,
            this.btnOpen,
            this.toolStripSeparator2,
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator3,
            this.btnSelect,
            this.btnPencil});
            this.toolStripTools.Location = new System.Drawing.Point(0, 0);
            this.toolStripTools.Name = "toolStripTools";
            this.toolStripTools.Size = new System.Drawing.Size(945, 27);
            this.toolStripTools.TabIndex = 3;
            this.toolStripTools.Text = "toolStrip1";
            // 
            // btnLoadTool
            // 
            this.btnLoadTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadTool.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadTool.Image")));
            this.btnLoadTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadTool.Name = "btnLoadTool";
            this.btnLoadTool.Size = new System.Drawing.Size(24, 24);
            this.btnLoadTool.Text = "Load Tools";
            this.btnLoadTool.Click += new System.EventHandler(this.btnLoadTool_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnNewFile
            // 
            this.btnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewFile.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFile.Image")));
            this.btnNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(24, 24);
            this.btnNewFile.Text = "Create New File";
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 24);
            this.btnSave.Text = "Save File";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(24, 24);
            this.btnOpen.Text = "Open File";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(24, 24);
            this.btnUndo.Text = "Undo Action";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = ((System.Drawing.Image)(resources.GetObject("btnRedo.Image")));
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(24, 24);
            this.btnRedo.Text = "Redo Action";
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // btnSelect
            // 
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(24, 24);
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnPencil
            // 
            this.btnPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPencil.Image = ((System.Drawing.Image)(resources.GetObject("btnPencil.Image")));
            this.btnPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPencil.Name = "btnPencil";
            this.btnPencil.Size = new System.Drawing.Size(24, 24);
            this.btnPencil.Text = "Pencil Tool";
            this.btnPencil.Click += new System.EventHandler(this.btnPencil_Click);
            // 
            // openDllDialog
            // 
            this.openDllDialog.Filter = "tool files (*.dll)|*.dll|All files (*.*)|*.*";
            // 
            // panelSettings
            // 
            this.panelSettings.Controls.Add(this.buttonCreateAnimation);
            this.panelSettings.Controls.Add(this.button_Load);
            this.panelSettings.Controls.Add(this.label1);
            this.panelSettings.Controls.Add(this.buttonRepeat);
            this.panelSettings.Controls.Add(this.buttonStop);
            this.panelSettings.Controls.Add(this.buttonPlay);
            this.panelSettings.Controls.Add(this.labelMainColor);
            this.panelSettings.Controls.Add(this.labelFillColor);
            this.panelSettings.Controls.Add(this.btnFillColor);
            this.panelSettings.Controls.Add(this.btnMainColor);
            this.panelSettings.Controls.Add(this.labelLineThickness);
            this.panelSettings.Controls.Add(this.trackBarLineThickness);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSettings.Location = new System.Drawing.Point(863, 0);
            this.panelSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(82, 499);
            this.panelSettings.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "Animation\r\ncontrols\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRepeat
            // 
            this.buttonRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRepeat.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonRepeat.Image = ((System.Drawing.Image)(resources.GetObject("buttonRepeat.Image")));
            this.buttonRepeat.Location = new System.Drawing.Point(56, 381);
            this.buttonRepeat.Name = "buttonRepeat";
            this.buttonRepeat.Size = new System.Drawing.Size(25, 25);
            this.buttonRepeat.TabIndex = 14;
            this.buttonRepeat.UseVisualStyleBackColor = true;
            this.buttonRepeat.Click += new System.EventHandler(this.buttonRepeat_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStop.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
            this.buttonStop.Location = new System.Drawing.Point(28, 381);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(25, 25);
            this.buttonStop.TabIndex = 13;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPlay.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonPlay.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlay.Image")));
            this.buttonPlay.Location = new System.Drawing.Point(0, 381);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(25, 25);
            this.buttonPlay.TabIndex = 12;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // labelMainColor
            // 
            this.labelMainColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMainColor.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMainColor.Location = new System.Drawing.Point(0, 0);
            this.labelMainColor.Name = "labelMainColor";
            this.labelMainColor.Size = new System.Drawing.Size(82, 18);
            this.labelMainColor.TabIndex = 9;
            this.labelMainColor.Text = "Main Color";
            this.labelMainColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFillColor
            // 
            this.labelFillColor.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFillColor.Location = new System.Drawing.Point(7, 94);
            this.labelFillColor.Name = "labelFillColor";
            this.labelFillColor.Size = new System.Drawing.Size(72, 18);
            this.labelFillColor.TabIndex = 11;
            this.labelFillColor.Text = "Fill Color";
            this.labelFillColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFillColor
            // 
            this.btnFillColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillColor.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFillColor.Image = ((System.Drawing.Image)(resources.GetObject("btnFillColor.Image")));
            this.btnFillColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFillColor.Location = new System.Drawing.Point(7, 104);
            this.btnFillColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(69, 66);
            this.btnFillColor.TabIndex = 10;
            this.btnFillColor.UseVisualStyleBackColor = true;
            this.btnFillColor.Click += new System.EventHandler(this.btnFillColor_Click);
            // 
            // btnMainColor
            // 
            this.btnMainColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainColor.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMainColor.Image = ((System.Drawing.Image)(resources.GetObject("btnMainColor.Image")));
            this.btnMainColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMainColor.Location = new System.Drawing.Point(6, 12);
            this.btnMainColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMainColor.Name = "btnMainColor";
            this.btnMainColor.Size = new System.Drawing.Size(69, 64);
            this.btnMainColor.TabIndex = 8;
            this.btnMainColor.UseVisualStyleBackColor = true;
            this.btnMainColor.Click += new System.EventHandler(this.btnMainColor_Click);
            // 
            // labelLineThickness
            // 
            this.labelLineThickness.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLineThickness.Location = new System.Drawing.Point(7, 172);
            this.labelLineThickness.Name = "labelLineThickness";
            this.labelLineThickness.Size = new System.Drawing.Size(72, 35);
            this.labelLineThickness.TabIndex = 7;
            this.labelLineThickness.Text = "Line\r\nWidth\r\n";
            this.labelLineThickness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarLineThickness
            // 
            this.trackBarLineThickness.AutoSize = false;
            this.trackBarLineThickness.LargeChange = 10;
            this.trackBarLineThickness.Location = new System.Drawing.Point(22, 192);
            this.trackBarLineThickness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBarLineThickness.Name = "trackBarLineThickness";
            this.trackBarLineThickness.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarLineThickness.Size = new System.Drawing.Size(42, 168);
            this.trackBarLineThickness.TabIndex = 6;
            this.trackBarLineThickness.Tag = "";
            this.trackBarLineThickness.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarLineThickness.Scroll += new System.EventHandler(this.trackBarLineThickness_Scroll);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(3, 2);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(686, 480);
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // panelPictureBox
            // 
            this.panelPictureBox.AutoScroll = true;
            this.panelPictureBox.Controls.Add(this.pictureBox);
            this.panelPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPictureBox.Location = new System.Drawing.Point(0, 0);
            this.panelPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPictureBox.Name = "panelPictureBox";
            this.panelPictureBox.Size = new System.Drawing.Size(863, 499);
            this.panelPictureBox.TabIndex = 9;
            // 
            // buttonCreateAnimation
            // 
            this.buttonCreateAnimation.Location = new System.Drawing.Point(2, 441);
            this.buttonCreateAnimation.Name = "buttonCreateAnimation";
            this.buttonCreateAnimation.Size = new System.Drawing.Size(79, 37);
            this.buttonCreateAnimation.TabIndex = 9;
            this.buttonCreateAnimation.Text = "Add new anim";
            this.buttonCreateAnimation.UseVisualStyleBackColor = true;
            this.buttonCreateAnimation.Click += new System.EventHandler(this.buttonCreateAnimation_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCanvaSize});
            this.statusStrip.Location = new System.Drawing.Point(0, 526);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(945, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabelCanvaSize
            // 
            this.statusLabelCanvaSize.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusLabelCanvaSize.Name = "statusLabelCanvaSize";
            this.statusLabelCanvaSize.Size = new System.Drawing.Size(0, 17);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelPictureBox);
            this.panelMain.Controls.Add(this.panelSettings);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 27);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(945, 499);
            this.panelMain.TabIndex = 11;
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(4, 412);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 16;
            this.button_Load.Text = "Load anim";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 548);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripTools);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "VectorPainter";
            this.toolStripTools.ResumeLayout(false);
            this.toolStripTools.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panelPictureBox.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripTools;
        private System.Windows.Forms.OpenFileDialog openDllDialog;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripButton btnPencil;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.TrackBar trackBarLineThickness;
        private System.Windows.Forms.Label labelFillColor;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.Label labelMainColor;
        private System.Windows.Forms.Button btnMainColor;
        private System.Windows.Forms.Label labelLineThickness;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panelPictureBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCanvaSize;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStripButton btnNewFile;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLoadTool;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.Button buttonRepeat;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCreateAnimation;
        private System.Windows.Forms.Button button_Load;
    }
}
