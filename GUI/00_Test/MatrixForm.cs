using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyAlgorithm;

namespace _00_Test
{
    public partial class MatrixForm : Form
    {
        public MainForm motherForm;
        AppendLogCallback AppendLog { get; set; }
        DrawImageSafelyCallback DrawImageSafely { get; set; }

        ushort min;
        ushort max;
        ushort scaleRatio;
        ushort rows;
        ushort colums;

        public MatrixForm(Form motherForm)
        {
            InitializeComponent();

            this.motherForm = motherForm as MainForm;

            this.AppendLog = this.motherForm.AppendLog;
            this.DrawImageSafely = this.motherForm.DrawImageSafely;

            this.min = this.motherForm.Min;
            this.max = this.motherForm.Max;
            this.rows = this.motherForm.Rows;
            this.colums = this.motherForm.Colums;
            this.scaleRatio = this.motherForm.ScaleRatio;

            Bitmap bar = MyGraphics.CreateColorBlend(20, 400, LinearGradientMode.Vertical);
            //picBar.Image = bar;
            bar.RotateFlip(RotateFlipType.Rotate180FlipX);
            DrawImageSafely?.Invoke(picBar, bar);
        }

        public void DrawMatrix(ushort[] data)
        {
            try
            {
                //MyArrayAlgo.FiltUshortData(data, min, max);

                ushort[] buf = new ushort[data.Length];
                Array.Copy(data, buf, buf.Length);

                MyArrayAlgo.FiltUshortData(buf, min, max);

                ushort[] dataFinger = new ushort[240];
                ushort[] dataPalm = new ushort[200];

                Array.Copy(buf, 0, dataFinger, 0, dataFinger.Length);
                Array.Copy(buf, dataFinger.Length, dataPalm, 0, dataPalm.Length);

                Bitmap bmFinger = MyGraphics.GenerateMatrixBitmap(dataFinger, colums, 24, min, max, scaleRatio);
                Bitmap bmPalm = MyGraphics.GenerateMatrixBitmap(dataPalm, colums, 20, min, max, scaleRatio);
                bmFinger.RotateFlip(RotateFlipType.Rotate270FlipNone);
                bmPalm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                DrawImageSafely?.Invoke(picFinger, bmFinger);
                DrawImageSafely?.Invoke(picPalm, bmPalm);
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from MatrixForm: " + nameof(DrawMatrix));
                AppendLog?.Invoke(ex.Message);
                this.Invoke(new EventHandler(motherForm.btnClose_Click));
                AppendLog(nameof(DrawMatrix) + " closed the port");
            }
        }
    }
}
