namespace _00_Test
{
    partial class DetectObjectForm
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
            this.components = new System.ComponentModel.Container();
            this.grSerial = new System.Windows.Forms.GroupBox();
            this.grSerialButton = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.grSerialSetup = new System.Windows.Forms.GroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbPortName = new System.Windows.Forms.ComboBox();
            this.cbbStopBits = new System.Windows.Forms.ComboBox();
            this.cbbBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbParityBits = new System.Windows.Forms.ComboBox();
            this.cbbDataSize = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.port2 = new System.IO.Ports.SerialPort(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grSerial.SuspendLayout();
            this.grSerialButton.SuspendLayout();
            this.grSerialSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grSerial
            // 
            this.grSerial.Controls.Add(this.grSerialButton);
            this.grSerial.Controls.Add(this.progressBar);
            this.grSerial.Controls.Add(this.grSerialSetup);
            this.grSerial.Location = new System.Drawing.Point(12, 12);
            this.grSerial.Name = "grSerial";
            this.grSerial.Size = new System.Drawing.Size(303, 263);
            this.grSerial.TabIndex = 36;
            this.grSerial.TabStop = false;
            this.grSerial.Text = "SERIAL";
            // 
            // grSerialButton
            // 
            this.grSerialButton.Controls.Add(this.btnClose);
            this.grSerialButton.Controls.Add(this.btnOpen);
            this.grSerialButton.Location = new System.Drawing.Point(153, 19);
            this.grSerialButton.Name = "grSerialButton";
            this.grSerialButton.Size = new System.Drawing.Size(147, 85);
            this.grSerialButton.TabIndex = 22;
            this.grSerialButton.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(72, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 68);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(6, 9);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(63, 68);
            this.btnOpen.TabIndex = 20;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(153, 110);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(147, 23);
            this.progressBar.TabIndex = 23;
            // 
            // grSerialSetup
            // 
            this.grSerialSetup.Controls.Add(this.lblName);
            this.grSerialSetup.Controls.Add(this.label5);
            this.grSerialSetup.Controls.Add(this.cbbPortName);
            this.grSerialSetup.Controls.Add(this.cbbStopBits);
            this.grSerialSetup.Controls.Add(this.cbbBaudRate);
            this.grSerialSetup.Controls.Add(this.label4);
            this.grSerialSetup.Controls.Add(this.label6);
            this.grSerialSetup.Controls.Add(this.cbbParityBits);
            this.grSerialSetup.Controls.Add(this.cbbDataSize);
            this.grSerialSetup.Controls.Add(this.label7);
            this.grSerialSetup.Location = new System.Drawing.Point(6, 19);
            this.grSerialSetup.Name = "grSerialSetup";
            this.grSerialSetup.Size = new System.Drawing.Size(141, 235);
            this.grSerialSetup.TabIndex = 21;
            this.grSerialSetup.TabStop = false;
            this.grSerialSetup.Text = "Setup";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Port Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Stop Bits";
            // 
            // cbbPortName
            // 
            this.cbbPortName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbPortName.FormattingEnabled = true;
            this.cbbPortName.Location = new System.Drawing.Point(9, 41);
            this.cbbPortName.Name = "cbbPortName";
            this.cbbPortName.Size = new System.Drawing.Size(121, 21);
            this.cbbPortName.TabIndex = 0;
            // 
            // cbbStopBits
            // 
            this.cbbStopBits.FormattingEnabled = true;
            this.cbbStopBits.Items.AddRange(new object[] {
            "One",
            "Two"});
            this.cbbStopBits.Location = new System.Drawing.Point(9, 201);
            this.cbbStopBits.Name = "cbbStopBits";
            this.cbbStopBits.Size = new System.Drawing.Size(121, 21);
            this.cbbStopBits.TabIndex = 12;
            this.cbbStopBits.Text = "One";
            // 
            // cbbBaudRate
            // 
            this.cbbBaudRate.FormattingEnabled = true;
            this.cbbBaudRate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "921600",
            "2250000"});
            this.cbbBaudRate.Location = new System.Drawing.Point(9, 81);
            this.cbbBaudRate.Name = "cbbBaudRate";
            this.cbbBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cbbBaudRate.TabIndex = 6;
            this.cbbBaudRate.Text = "115200";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Parity Bits";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Baud Rate";
            // 
            // cbbParityBits
            // 
            this.cbbParityBits.FormattingEnabled = true;
            this.cbbParityBits.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cbbParityBits.Location = new System.Drawing.Point(9, 161);
            this.cbbParityBits.Name = "cbbParityBits";
            this.cbbParityBits.Size = new System.Drawing.Size(121, 21);
            this.cbbParityBits.TabIndex = 10;
            this.cbbParityBits.Text = "None";
            // 
            // cbbDataSize
            // 
            this.cbbDataSize.FormattingEnabled = true;
            this.cbbDataSize.Items.AddRange(new object[] {
            "8",
            "7"});
            this.cbbDataSize.Location = new System.Drawing.Point(9, 121);
            this.cbbDataSize.Name = "cbbDataSize";
            this.cbbDataSize.Size = new System.Drawing.Size(121, 21);
            this.cbbDataSize.TabIndex = 8;
            this.cbbDataSize.Text = "8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Data Size";
            // 
            // port2
            // 
            this.port2.BaudRate = 115200;
            this.port2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.port2_DataReceived);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(321, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 247);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DetectObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 284);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grSerial);
            this.Name = "DetectObjectForm";
            this.Text = "DetectObjectForm";
            this.Load += new System.EventHandler(this.DetectObjectForm_Load);
            this.grSerial.ResumeLayout(false);
            this.grSerialButton.ResumeLayout(false);
            this.grSerialSetup.ResumeLayout(false);
            this.grSerialSetup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grSerial;
        private System.Windows.Forms.GroupBox grSerialButton;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox grSerialSetup;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbbPortName;
        public System.Windows.Forms.ComboBox cbbStopBits;
        public System.Windows.Forms.ComboBox cbbBaudRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cbbParityBits;
        public System.Windows.Forms.ComboBox cbbDataSize;
        private System.Windows.Forms.Label label7;
        private System.IO.Ports.SerialPort port2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}