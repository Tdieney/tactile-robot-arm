namespace _00_Test
{
    partial class MatrixForm
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
            this.grGraphics = new System.Windows.Forms.GroupBox();
            this.picPalm = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMin = new System.Windows.Forms.Label();
            this.picBar = new System.Windows.Forms.PictureBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.picFinger = new System.Windows.Forms.PictureBox();
            this.grGraphics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPalm)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).BeginInit();
            this.SuspendLayout();
            // 
            // grGraphics
            // 
            this.grGraphics.Controls.Add(this.picPalm);
            this.grGraphics.Controls.Add(this.groupBox1);
            this.grGraphics.Controls.Add(this.picFinger);
            this.grGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grGraphics.Location = new System.Drawing.Point(0, 0);
            this.grGraphics.Name = "grGraphics";
            this.grGraphics.Size = new System.Drawing.Size(597, 472);
            this.grGraphics.TabIndex = 2;
            this.grGraphics.TabStop = false;
            // 
            // picPalm
            // 
            this.picPalm.Location = new System.Drawing.Point(12, 250);
            this.picPalm.Name = "picPalm";
            this.picPalm.Size = new System.Drawing.Size(400, 200);
            this.picPalm.TabIndex = 15;
            this.picPalm.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMin);
            this.groupBox1.Controls.Add(this.picBar);
            this.groupBox1.Controls.Add(this.lblMax);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(509, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(85, 453);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(8, 415);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(24, 13);
            this.lblMin.TabIndex = 13;
            this.lblMin.Text = "Min";
            // 
            // picBar
            // 
            this.picBar.Location = new System.Drawing.Point(41, 28);
            this.picBar.Name = "picBar";
            this.picBar.Size = new System.Drawing.Size(20, 400);
            this.picBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBar.TabIndex = 12;
            this.picBar.TabStop = false;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(8, 28);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(27, 13);
            this.lblMax.TabIndex = 11;
            this.lblMax.Text = "Max";
            // 
            // picFinger
            // 
            this.picFinger.Location = new System.Drawing.Point(12, 44);
            this.picFinger.Name = "picFinger";
            this.picFinger.Size = new System.Drawing.Size(480, 200);
            this.picFinger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picFinger.TabIndex = 0;
            this.picFinger.TabStop = false;
            // 
            // MatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 472);
            this.Controls.Add(this.grGraphics);
            this.Name = "MatrixForm";
            this.Text = "Matrix Display";
            this.grGraphics.ResumeLayout(false);
            this.grGraphics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPalm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grGraphics;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.PictureBox picBar;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.PictureBox picFinger;
        private System.Windows.Forms.PictureBox picPalm;
    }
}