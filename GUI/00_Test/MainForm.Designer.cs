
namespace _00_Test
{
    partial class MainForm
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
            this.port1 = new System.IO.Ports.SerialPort(this.components);
            this.numMax = new System.Windows.Forms.NumericUpDown();
            this.numMin = new System.Windows.Forms.NumericUpDown();
            this.lblMaxInData = new System.Windows.Forms.Label();
            this.lblMinInData = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.numScaleRatio = new System.Windows.Forms.NumericUpDown();
            this.numColums = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grMatrix = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.grLog = new System.Windows.Forms.GroupBox();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numIntervalTimer2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lineChart1 = new MindFusion.Charting.WinForms.LineChart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnServoControl = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDisplayHand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDisplayMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDetectObject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLineChart = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScaleRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numColums)).BeginInit();
            this.grMatrix.SuspendLayout();
            this.grLog.SuspendLayout();
            this.grSerial.SuspendLayout();
            this.grSerialButton.SuspendLayout();
            this.grSerialSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIntervalTimer2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // port1
            // 
            this.port1.BaudRate = 115200;
            this.port1.ReadBufferSize = 10000;
            this.port1.ReceivedBytesThreshold = 164;
            // 
            // numMax
            // 
            this.numMax.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(6, 32);
            this.numMax.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.numMax.Name = "numMax";
            this.numMax.Size = new System.Drawing.Size(82, 20);
            this.numMax.TabIndex = 21;
            this.numMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMax.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // numMin
            // 
            this.numMin.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMin.Location = new System.Drawing.Point(6, 71);
            this.numMin.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.numMin.Name = "numMin";
            this.numMin.Size = new System.Drawing.Size(82, 20);
            this.numMin.TabIndex = 22;
            this.numMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMin.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // lblMaxInData
            // 
            this.lblMaxInData.AutoSize = true;
            this.lblMaxInData.Location = new System.Drawing.Point(6, 16);
            this.lblMaxInData.Name = "lblMaxInData";
            this.lblMaxInData.Size = new System.Drawing.Size(57, 13);
            this.lblMaxInData.TabIndex = 23;
            this.lblMaxInData.Text = "Max Value";
            // 
            // lblMinInData
            // 
            this.lblMinInData.AutoSize = true;
            this.lblMinInData.Location = new System.Drawing.Point(6, 55);
            this.lblMinInData.Name = "lblMinInData";
            this.lblMinInData.Size = new System.Drawing.Size(54, 13);
            this.lblMinInData.TabIndex = 24;
            this.lblMinInData.Text = "Min Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Scale Ratio";
            // 
            // numRows
            // 
            this.numRows.Location = new System.Drawing.Point(6, 110);
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(82, 20);
            this.numRows.TabIndex = 25;
            this.numRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRows.Value = new decimal(new int[] {
            44,
            0,
            0,
            0});
            // 
            // numScaleRatio
            // 
            this.numScaleRatio.Location = new System.Drawing.Point(6, 188);
            this.numScaleRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numScaleRatio.Name = "numScaleRatio";
            this.numScaleRatio.Size = new System.Drawing.Size(82, 20);
            this.numScaleRatio.TabIndex = 29;
            this.numScaleRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numScaleRatio.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numColums
            // 
            this.numColums.Location = new System.Drawing.Point(6, 149);
            this.numColums.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColums.Name = "numColums";
            this.numColums.Size = new System.Drawing.Size(82, 20);
            this.numColums.TabIndex = 26;
            this.numColums.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numColums.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Colums";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Rows";
            // 
            // grMatrix
            // 
            this.grMatrix.Controls.Add(this.numMax);
            this.grMatrix.Controls.Add(this.label2);
            this.grMatrix.Controls.Add(this.numMin);
            this.grMatrix.Controls.Add(this.label1);
            this.grMatrix.Controls.Add(this.lblMaxInData);
            this.grMatrix.Controls.Add(this.numColums);
            this.grMatrix.Controls.Add(this.lblMinInData);
            this.grMatrix.Controls.Add(this.numScaleRatio);
            this.grMatrix.Controls.Add(this.label3);
            this.grMatrix.Controls.Add(this.numRows);
            this.grMatrix.Location = new System.Drawing.Point(309, 24);
            this.grMatrix.Name = "grMatrix";
            this.grMatrix.Size = new System.Drawing.Size(109, 217);
            this.grMatrix.TabIndex = 31;
            this.grMatrix.TabStop = false;
            this.grMatrix.Text = "MATRIX";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 16);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(367, 248);
            this.txtLog.TabIndex = 33;
            this.txtLog.Text = "version 15062022 00h02\n\n";
            // 
            // grLog
            // 
            this.grLog.Controls.Add(this.txtLog);
            this.grLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.grLog.Location = new System.Drawing.Point(423, 24);
            this.grLog.Name = "grLog";
            this.grLog.Size = new System.Drawing.Size(373, 267);
            this.grLog.TabIndex = 34;
            this.grLog.TabStop = false;
            this.grLog.Text = "LOG";
            // 
            // grSerial
            // 
            this.grSerial.Controls.Add(this.grSerialButton);
            this.grSerial.Controls.Add(this.progressBar);
            this.grSerial.Controls.Add(this.grSerialSetup);
            this.grSerial.Location = new System.Drawing.Point(0, 24);
            this.grSerial.Name = "grSerial";
            this.grSerial.Size = new System.Drawing.Size(303, 263);
            this.grSerial.TabIndex = 35;
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
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numIntervalTimer2
            // 
            this.numIntervalTimer2.Location = new System.Drawing.Point(315, 263);
            this.numIntervalTimer2.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numIntervalTimer2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numIntervalTimer2.Name = "numIntervalTimer2";
            this.numIntervalTimer2.Size = new System.Drawing.Size(82, 20);
            this.numIntervalTimer2.TabIndex = 36;
            this.numIntervalTimer2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numIntervalTimer2.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(315, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Time Interval (ms)";
            // 
            // lineChart1
            // 
            this.lineChart1.LegendTitle = "Legend";
            this.lineChart1.Location = new System.Drawing.Point(0, 0);
            this.lineChart1.Name = "lineChart1";
            this.lineChart1.Padding = new System.Windows.Forms.Padding(5);
            this.lineChart1.ShowLegend = true;
            this.lineChart1.Size = new System.Drawing.Size(384, 256);
            this.lineChart1.SubtitleFontName = null;
            this.lineChart1.SubtitleFontSize = null;
            this.lineChart1.SubtitleFontStyle = null;
            this.lineChart1.TabIndex = 0;
            this.lineChart1.Text = "lineChart1";
            this.lineChart1.Theme.UniformSeriesFill = new MindFusion.Drawing.SolidBrush("#FF90EE90");
            this.lineChart1.Theme.UniformSeriesStroke = new MindFusion.Drawing.SolidBrush("#FF000000");
            this.lineChart1.Theme.UniformSeriesStrokeThickness = 2D;
            this.lineChart1.TitleFontName = null;
            this.lineChart1.TitleFontSize = null;
            this.lineChart1.TitleFontStyle = null;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnServoControl,
            this.mnDisplay,
            this.mnDetectObject,
            this.mnLineChart});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 38;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnServoControl
            // 
            this.mnServoControl.Name = "mnServoControl";
            this.mnServoControl.Size = new System.Drawing.Size(91, 20);
            this.mnServoControl.Text = "Servo Control";
            this.mnServoControl.Click += new System.EventHandler(this.mnServoControl_Click);
            // 
            // mnDisplay
            // 
            this.mnDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnDisplayHand,
            this.mnDisplayMatrix});
            this.mnDisplay.Name = "mnDisplay";
            this.mnDisplay.Size = new System.Drawing.Size(57, 20);
            this.mnDisplay.Text = "Display";
            // 
            // mnDisplayHand
            // 
            this.mnDisplayHand.Name = "mnDisplayHand";
            this.mnDisplayHand.Size = new System.Drawing.Size(108, 22);
            this.mnDisplayHand.Text = "Hand";
            this.mnDisplayHand.Click += new System.EventHandler(this.mnDisplayHand_Click);
            // 
            // mnDisplayMatrix
            // 
            this.mnDisplayMatrix.Name = "mnDisplayMatrix";
            this.mnDisplayMatrix.Size = new System.Drawing.Size(108, 22);
            this.mnDisplayMatrix.Text = "Matrix";
            this.mnDisplayMatrix.Click += new System.EventHandler(this.mnDisplayMatrix_Click);
            // 
            // mnDetectObject
            // 
            this.mnDetectObject.Name = "mnDetectObject";
            this.mnDetectObject.Size = new System.Drawing.Size(91, 20);
            this.mnDetectObject.Text = "Detect Object";
            this.mnDetectObject.Click += new System.EventHandler(this.mnDetectObject_Click);
            // 
            // mnLineChart
            // 
            this.mnLineChart.Name = "mnLineChart";
            this.mnLineChart.Size = new System.Drawing.Size(70, 20);
            this.mnLineChart.Text = "LineChart";
            this.mnLineChart.Click += new System.EventHandler(this.mnLineChart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 291);
            this.Controls.Add(this.numIntervalTimer2);
            this.Controls.Add(this.grSerial);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.grLog);
            this.Controls.Add(this.grMatrix);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScaleRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numColums)).EndInit();
            this.grMatrix.ResumeLayout(false);
            this.grMatrix.PerformLayout();
            this.grLog.ResumeLayout(false);
            this.grSerial.ResumeLayout(false);
            this.grSerialButton.ResumeLayout(false);
            this.grSerialSetup.ResumeLayout(false);
            this.grSerialSetup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIntervalTimer2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.IO.Ports.SerialPort port1;
        private System.Windows.Forms.NumericUpDown numMax;
        private System.Windows.Forms.NumericUpDown numMin;
        private System.Windows.Forms.Label lblMaxInData;
        private System.Windows.Forms.Label lblMinInData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.NumericUpDown numScaleRatio;
        private System.Windows.Forms.NumericUpDown numColums;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grMatrix;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.GroupBox grLog;
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numIntervalTimer2;
        private System.Windows.Forms.Label label8;
        private MindFusion.Charting.WinForms.LineChart lineChart1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnServoControl;
        private System.Windows.Forms.ToolStripMenuItem mnDisplay;
        private System.Windows.Forms.ToolStripMenuItem mnDisplayHand;
        private System.Windows.Forms.ToolStripMenuItem mnDisplayMatrix;
        private System.Windows.Forms.ToolStripMenuItem mnDetectObject;
        public System.Windows.Forms.ToolStripMenuItem mnLineChart;
    }
}

