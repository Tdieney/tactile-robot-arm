using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _00_Test
{
    public partial class DetectObjectForm : Form
    {
        MainForm motherForm;
        public AppendLogCallback AppendLog;

        public DetectObjectForm(Form motherForm)
        {
            this.motherForm = motherForm as MainForm;
            this.AppendLog = this.motherForm.AppendLog;

            InitializeComponent();
        }

        private void DetectObjectForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        public void ChangeNumSafely(NumericUpDown num, int val)
        {
            if (num.InvokeRequired)
            {
                Action safeDraw = delegate { ChangeNumSafely(num, val); };
                this.Invoke(safeDraw);
            }
            else
                num.Value = val;
        }

        public void DrawImageSafely(PictureBox pic, Image img)
        {
            if (pic.InvokeRequired)
            {
                Action safeDraw = delegate { DrawImageSafely(pic, img); };
                this.Invoke(safeDraw);
            }
            else
                pic.Image = img;
        }

        private void port2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string str = port2.ReadExisting();

                NumericUpDown num = motherForm.fingerControl.numLevel;

                switch (str)
                {
                    case "nuocsuoi":
                        ChangeNumSafely(num, 3);
                        DrawImageSafely(pictureBox1, Image.FromFile("images\\nuocsuoi.jpg"));
                        break;
                    case "nuocmuoi":
                        ChangeNumSafely(num, 4);
                        DrawImageSafely(pictureBox1, Image.FromFile("images\\nuocmuoi.jpg"));
                        break;
                    case "khoaitay":
                        ChangeNumSafely(num, 5);
                        DrawImageSafely(pictureBox1, Image.FromFile("images\\khoaitay.jpg"));
                        break;
                }
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from port2_DataReceived: " + nameof(port2_DataReceived));
                AppendLog?.Invoke(ex.Message);
            }
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
            }
            else
            {
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                grSerialSetup.Enabled = true;
                progressBar.Value = progressBar.Minimum;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (port2.IsOpen == false)
            {
                try
                {
                    port2.PortName = cbbPortName.Text;
                    port2.BaudRate = Int32.Parse(cbbBaudRate.Text);
                    port2.Parity = (Parity)Enum.Parse(typeof(Parity), cbbParityBits.Text);
                    port2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbbStopBits.Text);
                    port2.DataBits = Int32.Parse(cbbDataSize.Text);

                    port2.Open();
                    port2.DiscardInBuffer();
                    UpdateStatus(port2);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (port2.IsOpen == true)
            {
                try
                {
                    port2.Close();
                    UpdateStatus(port2);
                }
                catch (Exception ex)
                {
                }
            }
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
            }
        }
    }
}
