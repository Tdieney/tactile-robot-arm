using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MyAlgorithm;
using System.IO;
using DateTimeSeries;

namespace _00_Test
{
    public delegate void AppendLogCallback(string message);
    public delegate void DrawImageSafelyCallback(PictureBox pic, Bitmap bm);
    public partial class MainForm : Form
    {
        public CancellationTokenSource cts;

        public ServoControlForm fingerControl;
        public HandForm handDisplay;
        public MatrixForm matrixDisplay;
        public DetectObjectForm detectObjectForm;
        public LineChartForm lineChartForm;

        //delegate
        public ushort Max { get; set; }
        public ushort Min { get; set; }
        public ushort Rows { get; set; }
        public ushort Colums { get; set; }
        public ushort ScaleRatio { get; set; }
        public int PackageSize { get { return (Rows * Colums + 2) * 2; } }
        public int RealPackageSize { get { return Rows * Colums * 2; } }

        public Stopwatch sw = new Stopwatch();
        public MainForm()
        {
            InitializeComponent();

            Binding maxBinding = new Binding("Max", numMax, "Value", true, DataSourceUpdateMode.Never);
            this.DataBindings.Add(maxBinding);
            Binding minBinding = new Binding("Min", numMin, "Value", true, DataSourceUpdateMode.Never);
            this.DataBindings.Add(minBinding);
            Binding rowsBinding = new Binding("Rows", numRows, "Value", true, DataSourceUpdateMode.Never);
            this.DataBindings.Add(rowsBinding);
            Binding columsBinding = new Binding("Colums", numColums, "Value", true, DataSourceUpdateMode.Never);
            this.DataBindings.Add(columsBinding);
            Binding scaleRatioBinding = new Binding("ScaleRatio", numScaleRatio, "Value", true, DataSourceUpdateMode.Never);
            this.DataBindings.Add(scaleRatioBinding);

            timer1.Start();
        }

        public void AppendLog(string message)
        {
            if (message != null)
            {
                if (this.txtLog.InvokeRequired)
                {
                    AppendLogCallback d = new AppendLogCallback(AppendLog);
                    this.Invoke(d, new object[] { message });
                }
                else
                    this.txtLog.AppendText(message + "\n");
            }
        }
        public void DrawImageSafely(PictureBox pic, Bitmap bm)
        {
            if (pic.InvokeRequired)
            {
                Action safeDraw = delegate { DrawImageSafely(pic, bm); };
                this.Invoke(safeDraw);
            }
            else
                pic.Image = bm;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    Array.Sort(ports);
                    cbbPortName.Items.Clear();
                    cbbPortName.Items.AddRange(ports);
                    if (cbbPortName.Text == "")
                        cbbPortName.Text = ports[0];
                }
            }
            catch (Exception ex)
            {
                AppendLog("Message from Serial Form: " + nameof(timer1_Tick));
                AppendLog(ex.Message);
            }
        }
        public void ContinousRead(SerialPort port, CancellationToken token)
        {
            try
            {
                int bytesRead, num, offset = 0;
                byte[] buf = new byte[RealPackageSize];
                ushort[] dataArray = new ushort[Rows * Colums];
                byte[] buffer = new byte[PackageSize * 2];
                List<int> listIndx;

                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep((int)numIntervalTimer2.Value);

                    if (port.IsOpen)
                    {
                        bytesRead = port.BytesToRead;
                        if (bytesRead > 0)
                        {
                            if (bytesRead > buffer.Length - offset)
                                bytesRead = buffer.Length - offset;

                            num = port.Read(buffer, offset, bytesRead);
                            offset += num;
                            if (offset >= buffer.Length)
                                offset = 0;

                            listIndx = MyArrayAlgo.FindIndex(buffer, PackageSize, 0xaa, 0x55);
                            if (listIndx.Count > 0)
                            {
                                foreach (int indx in listIndx)
                                {
                                    Array.Copy(buffer, indx + 2, buf, 0, buf.Length);
                                    //draw
                                    DataHandler(buf);
                                }

                                offset = 0;
                                port.DiscardInBuffer();
                                Array.Clear(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLog("Message from MainForm: " + nameof(ContinousRead));
                AppendLog(ex.Message);
                this.Invoke(new EventHandler(btnClose_Click));
                AppendLog(nameof(ContinousRead) + " closed the port");
            }
        }

        private void DataHandler(byte[] buf)
        {
            try
            {
                ushort[] dataArray = new ushort[Rows * Colums];
                Buffer.BlockCopy(buf, 0, dataArray, 0, buf.Length);
                //draw
                if (handDisplay != null && !handDisplay.IsDisposed)
                    handDisplay.DrawHand(dataArray);
                if (matrixDisplay != null && !matrixDisplay.IsDisposed)
                    matrixDisplay.DrawMatrix(dataArray);
            }
            catch (Exception ex)
            {
                AppendLog("Message from MainForm: " + nameof(DataHandler));
                AppendLog(ex.Message);
            }
        }

        private void btnFingerControl_Click(object sender, EventArgs e)
        {
            if (fingerControl == null || fingerControl.IsDisposed)
            {
                fingerControl = new ServoControlForm(this);
                fingerControl.Show();
            }
            else if (!fingerControl.IsAccessible)
                fingerControl.Activate();
        }

        private void btnHandDisplay_Click(object sender, EventArgs e)
        {
            if (handDisplay == null || handDisplay.IsDisposed)
            {
                handDisplay = new HandForm(this);
                handDisplay.Show();
            }
            else if (!handDisplay.IsAccessible)
                handDisplay.Activate();
        }

        private void btnMatrixDisplay_Click(object sender, EventArgs e)
        {
            if (matrixDisplay == null || matrixDisplay.IsDisposed)
            {
                matrixDisplay = new MatrixForm(this);
                matrixDisplay.Show();
            }
            else if (!matrixDisplay.IsAccessible)
                matrixDisplay.Activate();
        }

        public void UpdateStatus(SerialPort port)
        {
            if (port.IsOpen)
            {
                cbbPortName.Text = port.PortName;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                grSerialSetup.Enabled = false;
                progressBar.Value = progressBar.Maximum;
                grMatrix.Enabled = false;

                //numIntervalTimer2.Enabled = false;
            }
            else
            {
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                grSerialSetup.Enabled = true;
                progressBar.Value = progressBar.Minimum;
                grMatrix.Enabled = true;

                //numIntervalTimer2.Enabled = true;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (port1.IsOpen == false)
            {
                try
                {
                    port1.PortName = cbbPortName.Text;
                    port1.BaudRate = Int32.Parse(cbbBaudRate.Text);
                    port1.Parity = (Parity)Enum.Parse(typeof(Parity), cbbParityBits.Text);
                    port1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbbStopBits.Text);
                    port1.DataBits = Int32.Parse(cbbDataSize.Text);

                    port1.Open();
                    port1.DiscardInBuffer();
                    UpdateStatus(port1);

                    cts = new CancellationTokenSource();
                    Task task1 = new Task(() => ContinousRead(this.port1, cts.Token), cts.Token);
                    task1.Start();
                }
                catch (Exception ex)
                {
                    AppendLog("Message from MainForm: " + nameof(btnOpen_Click));
                    AppendLog(ex.Message);
                }
            }
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            if (port1.IsOpen == true)
            {
                try
                {
                    cts.Cancel();

                    port1.Close();
                    UpdateStatus(port1);

                    //clear 
                    ushort[] dataArray = new ushort[Rows * Colums];
                    Array.Clear(dataArray, 0, dataArray.Length);
                    //draw
                    if (handDisplay != null && !handDisplay.IsDisposed)
                        handDisplay.DrawHand(dataArray);
                    if (matrixDisplay != null && !matrixDisplay.IsDisposed)
                        matrixDisplay.DrawMatrix(dataArray);
                }
                catch (Exception ex)
                {
                    AppendLog("Message from MainForm: " + nameof(btnClose_Click));
                    AppendLog(ex.Message);
                }
            }
        }

        private void mnServoControl_Click(object sender, EventArgs e)
        {
            if (fingerControl == null || fingerControl.IsDisposed)
            {
                fingerControl = new ServoControlForm(this);
                fingerControl.Show();
            }
            else if (!fingerControl.IsAccessible)
                fingerControl.Activate();
        }

        private void mnDisplayHand_Click(object sender, EventArgs e)
        {
            if (handDisplay == null || handDisplay.IsDisposed)
            {
                handDisplay = new HandForm(this);
                handDisplay.Show();
            }

            if (!handDisplay.IsAccessible)
                handDisplay.Activate();
        }

        private void mnDisplayMatrix_Click(object sender, EventArgs e)
        {
            if (matrixDisplay == null || matrixDisplay.IsDisposed)
            {
                matrixDisplay = new MatrixForm(this);
                matrixDisplay.Show();
            }
            else if (!matrixDisplay.IsAccessible)
                matrixDisplay.Activate();
        }

        private void mnLineChart_Click(object sender, EventArgs e)
        {
            if (handDisplay != null && !handDisplay.IsDisposed)
            {
                if (lineChartForm == null || lineChartForm.IsDisposed)
                {
                    lineChartForm = new LineChartForm(this);
                    lineChartForm.Show();
                }
                else if (!lineChartForm.IsAccessible)
                    lineChartForm.Activate();
            }
        }

        private void mnDetectObject_Click(object sender, EventArgs e)
        {
            if (detectObjectForm == null || detectObjectForm.IsDisposed)
            {
                detectObjectForm = new DetectObjectForm(this);
                detectObjectForm.Show();
            }
            else if (!detectObjectForm.IsAccessible)
                detectObjectForm.Activate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mnLineChart.Enabled = false;
        }
    }
}
