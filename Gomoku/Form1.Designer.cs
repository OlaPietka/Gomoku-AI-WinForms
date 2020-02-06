namespace Gomoku
{
    partial class Form1
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
            this.skControl = new SkiaSharp.Views.Desktop.SKControl();
            this.blackPoints = new System.Windows.Forms.Label();
            this.pinkPoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skControl
            // 
            this.skControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skControl.Location = new System.Drawing.Point(3, 56);
            this.skControl.Name = "skControl";
            this.skControl.Size = new System.Drawing.Size(1000, 915);
            this.skControl.TabIndex = 0;
            this.skControl.Text = "skControl1";
            this.skControl.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.PaintSurface);
            this.skControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SkControl_DoubleClick);
            this.skControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SkControl_MouseDown);
            this.skControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SkControl_MouseMove);
            this.skControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SkControl_MouseUp);
            // 
            // blackPoints
            // 
            this.blackPoints.AutoSize = true;
            this.blackPoints.Location = new System.Drawing.Point(12, 9);
            this.blackPoints.Name = "blackPoints";
            this.blackPoints.Size = new System.Drawing.Size(0, 25);
            this.blackPoints.TabIndex = 1;
            // 
            // pinkPoints
            // 
            this.pinkPoints.AutoSize = true;
            this.pinkPoints.Location = new System.Drawing.Point(740, 9);
            this.pinkPoints.Name = "pinkPoints";
            this.pinkPoints.Size = new System.Drawing.Size(0, 25);
            this.pinkPoints.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 979);
            this.Controls.Add(this.pinkPoints);
            this.Controls.Add(this.blackPoints);
            this.Controls.Add(this.skControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKControl skControl;
        private System.Windows.Forms.Label blackPoints;
        private System.Windows.Forms.Label pinkPoints;
    }
}

