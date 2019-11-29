namespace MiniDraw
{
    partial class DrawForm
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
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.fillButton = new System.Windows.Forms.ToolStripButton();
            this.clearAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AutoSize = true;
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.canvas.Location = new System.Drawing.Point(33, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(871, 512);
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.redoButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.fillButton,
            this.toolStripSeparator2,
            this.clearAllButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(40, 512);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip";
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = global::MiniDraw.Properties.Resources.redo_arrow;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(27, 24);
            this.undoButton.Text = "Redo 3 last";
            this.undoButton.ToolTipText = "Redo 3 last items";
            this.undoButton.Click += new System.EventHandler(this.RedoLastThree);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = global::MiniDraw.Properties.Resources.redo_arrow;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(27, 24);
            this.redoButton.Text = "Redo";
            this.redoButton.Click += new System.EventHandler(this.Redo);
            // 
            // fillButton
            // 
            this.fillButton.CheckOnClick = true;
            this.fillButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fillButton.Image = global::MiniDraw.Properties.Resources.paint_bucket;
            this.fillButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillButton.Name = "fillButton";
            this.fillButton.Size = new System.Drawing.Size(27, 24);
            this.fillButton.Text = "Fill rectangles with red";
            this.fillButton.CheckedChanged += new System.EventHandler(this.FillCheckboxStateChanged);
            // 
            // clearAllButton
            // 
            this.clearAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearAllButton.Image = global::MiniDraw.Properties.Resources.clear;
            this.clearAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(27, 24);
            this.clearAllButton.Text = "Clear all";
            this.clearAllButton.Click += new System.EventHandler(this.ClearAll);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MiniDraw.Properties.Resources.undo_arrow;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(37, 24);
            this.toolStripButton1.Text = "Undo";
            this.toolStripButton1.Click += new System.EventHandler(this.Undo);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(27, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(27, 6);
            // 
            // DrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(904, 512);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.canvas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DrawForm";
            this.Text = "MiniDraw";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripButton fillButton;
        private System.Windows.Forms.ToolStripButton clearAllButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

