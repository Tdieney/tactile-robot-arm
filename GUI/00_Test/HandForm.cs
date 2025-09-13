using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyAlgorithm;

namespace _00_Test
{
    public partial class HandForm : Form
    {
        public MainForm motherForm;
        public AppendLogCallback AppendLog;
        public DrawImageSafelyCallback DrawImageSafely;

        ushort min;
        ushort max;
        ushort rows;
        ushort colums;
        ushort scaleRatio;

        MyUshortArray2 bot1, top1, bot2, top2, bot3, top3, palm;
        Bitmap bot1Bm, top1Bm, bot2Bm, top2Bm, bot3Bm, top3Bm, palmBm;
        ushort peakBot1, peakBot2, peakBot3, peakTop1, peakTop2, peakTop3, peakPalm;

 

        public ushort PeakBot1 { get => peakBot1; set => peakBot1 = value; }
        public ushort PeakBot2 { get => peakBot2; set => peakBot2 = value; }
        public ushort PeakBot3 { get => peakBot3; set => peakBot3 = value; }
        public ushort PeakTop1 { get => peakTop1; set => peakTop1 = value; }
        public ushort PeakTop2 { get => peakTop2; set => peakTop2 = value; }
        public ushort PeakTop3 { get => peakTop3; set => peakTop3 = value; }
        public ushort PeakPalm { get => peakPalm; set => peakPalm = value; }


        int failClick = 0;
        int passClick = 0;
        private void btnFail_Click(object sender, EventArgs e)
        {
            failClick = 1;
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            passClick = 1;
        }

        Stopwatch sw = new Stopwatch();

        public HandForm(Form motherForm)
        {
            InitializeComponent();

            this.motherForm = motherForm as MainForm;

            this.AppendLog = this.motherForm.AppendLog;
            this.DrawImageSafely = this.motherForm.DrawImageSafely;

            min = this.motherForm.Min;
            max = this.motherForm.Max;
            rows = this.motherForm.Rows;
            colums = this.motherForm.Colums;
            scaleRatio = this.motherForm.ScaleRatio;

            Bitmap bar = MyGraphics.CreateColorBlend(20, 400, LinearGradientMode.Vertical);
            bar.RotateFlip(RotateFlipType.Rotate180FlipNone);
            DrawImageSafely?.Invoke(picBar, bar);
        }

        private void HandForm_Load(object sender, EventArgs e)
        {
            motherForm.mnLineChart.Enabled = true;
        }

        private void HandForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            motherForm.mnLineChart.Enabled = false;
        }

        public void DrawHand(ushort[] data)
        {
            try
            {
                ushort[] buf = new ushort[data.Length];
                Array.Copy(data, buf, buf.Length);

                MyUshortArray2 matrix;
                matrix = MyArrayAlgo.ConvertArray1ToArray2(buf, 10, 44);

                bot1 = MyArrayAlgo.SplitArray2(matrix, 5, 0, 5, 8);
                top1 = MyArrayAlgo.SplitArray2(matrix, 0, 0, 5, 8);
                bot2 = MyArrayAlgo.SplitArray2(matrix, 5, 8, 5, 8);
                top2 = MyArrayAlgo.SplitArray2(matrix, 0, 8, 5, 8);
                bot3 = MyArrayAlgo.SplitArray2(matrix, 5, 16, 5, 8);
                top3 = MyArrayAlgo.SplitArray2(matrix, 0, 16, 5, 8);
                palm = MyArrayAlgo.SplitArray2(matrix, 0, 24, 10, 20);

                //calculate sum
                ulong sumBot1, sumBot2, sumBot3, sumTop1, sumTop2, sumTop3, sumPalm;
                sumBot1 = MyArrayAlgo.CalculateSum(bot1, 0, max);
                sumBot2 = MyArrayAlgo.CalculateSum(bot2, 0, max);
                sumBot3 = MyArrayAlgo.CalculateSum(bot3, 0, max);
                sumTop1 = MyArrayAlgo.CalculateSum(top1, 0, max);
                sumTop2 = MyArrayAlgo.CalculateSum(top2, 0, max);
                sumTop3 = MyArrayAlgo.CalculateSum(top3, 0, max);
                sumPalm = MyArrayAlgo.CalculateSum(palm, 0, max);

                peakBot1 = MyArrayAlgo.FindMax(bot1);
                peakBot2 = MyArrayAlgo.FindMax(bot2);
                peakBot3 = MyArrayAlgo.FindMax(bot3);
                peakTop1 = MyArrayAlgo.FindMax(top1);
                peakTop2 = MyArrayAlgo.FindMax(top2);
                peakTop3 = MyArrayAlgo.FindMax(top3);
                peakPalm = MyArrayAlgo.FindMax(palm);


                AddLabel(lblBot1, "sum: " + sumBot1 + "\npeak: " + peakBot1);
                AddLabel(lblBot2, "sum: " + sumBot2 + "\npeak: " + peakBot2);
                AddLabel(lblBot3, "sum: " + sumBot3 + "\npeak: " + peakBot3);
                AddLabel(lblTop1, "sum: " + sumTop1 + "\npeak: " + peakTop1);
                AddLabel(lblTop2, "sum: " + sumTop2 + "\npeak: " + peakTop2);
                AddLabel(lblTop3, "sum: " + sumTop3 + "\npeak: " + peakTop3);
                AddLabel(lblPalm, "sum: " + sumPalm + "\npeak: " + peakPalm);

                bot1Bm = MyGraphics.GenerateMatrixBitmap2(bot1, min, max, scaleRatio);
                top1Bm = MyGraphics.GenerateMatrixBitmap2(top1, min, max, scaleRatio);
                bot2Bm = MyGraphics.GenerateMatrixBitmap2(bot2, min, max, scaleRatio);
                top2Bm = MyGraphics.GenerateMatrixBitmap2(top2, min, max, scaleRatio);
                bot3Bm = MyGraphics.GenerateMatrixBitmap2(bot3, min, max, scaleRatio);
                top3Bm = MyGraphics.GenerateMatrixBitmap2(top3, min, max, scaleRatio);
                palmBm = MyGraphics.GenerateMatrixBitmap2(palm, min, max, scaleRatio);

                bot1Bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                top1Bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                bot2Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                top2Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                bot3Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                top3Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                palmBm.RotateFlip(RotateFlipType.Rotate180FlipY);


                //draw image
                DrawImageSafely?.Invoke(picBot1, bot1Bm);
                DrawImageSafely?.Invoke(picTop1, top1Bm);
                DrawImageSafely?.Invoke(picBot2, bot2Bm);
                DrawImageSafely?.Invoke(picTop2, top2Bm);
                DrawImageSafely?.Invoke(picBot3, bot3Bm);
                DrawImageSafely?.Invoke(picTop3, top3Bm);
                DrawImageSafely?.Invoke(picPalm, palmBm);


                //save data here
                if (passClick == 1)
                {
                    passClick = 0;
                    //Bitmap result = MergedBitmaps();
                    //result.Save("test.bmp");

                    MyUshortArray2 arr2 = MergeArray2();
                    arr2.WriteToCSVFile(txtFilename.Text, true);
                }

                if (failClick == 1)
                {
                    failClick = 0;
                    MyUshortArray2 arr2 = MergeArray2();
                    arr2.WriteToCSVFile(txtFilename.Text, false);
                }

                //sw.Stop();
                //motherForm.sw.Stop();

                //AppendLog?.Invoke(motherForm.sw.ElapsedMilliseconds.ToString());
                //AppendLog?.Invoke(sw.ElapsedMilliseconds.ToString());
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from HandForm: " + nameof(DrawHand));
                AppendLog?.Invoke(ex.Message);
                //this.Invoke(new EventHandler(motherForm.btnClose_Click));
                //AppendLog(nameof(DrawHand) + " closed the port");
            }
        }

        public void AddLabel(Label lbl, string text)
        {
            if (lbl.InvokeRequired)
            {
                Action safe = delegate { AddLabel(lbl, text); };
                this.Invoke(safe);
            }
            else
                lbl.Text = text;
        }

        //private Bitmap MergedBitmaps()
        //{
        //    Bitmap result = new Bitmap(top3Bm.Width + bot3Bm.Width
        //        + palmBm.Width + bot1Bm.Width + top1Bm.Width
        //        + scaleRatio * 4, palmBm.Height);

        //    //finger 3
        //    Point top3P = new Point(0, 0);
        //    Point bot3P = new Point(top3P.X + top3Bm.Width + scaleRatio, top3P.Y);
        //    //palm
        //    Point palmP = new Point(bot3P.X + bot3Bm.Width + scaleRatio, top3P.Y);
        //    //finger 2
        //    Point top2P = new Point(0, palmBm.Height - top2Bm.Height);
        //    Point bot2P = new Point(top2P.X + top2Bm.Width + scaleRatio, top2P.Y);
        //    //finger 1
        //    Point bot1P = new Point(palmP.X + palmBm.Width + scaleRatio, palmBm.Height / 2 - bot1Bm.Height / 2);
        //    Point top1P = new Point(bot1P.X + bot1Bm.Width + scaleRatio, bot1P.Y);

        //    using (Graphics g = Graphics.FromImage(result))
        //    {
        //        g.DrawImage(palmBm, palmP);
        //        g.DrawImage(bot1Bm, bot1P);
        //        g.DrawImage(bot2Bm, bot2P);
        //        g.DrawImage(bot3Bm, bot3P);
        //        g.DrawImage(top1Bm, top1P);
        //        g.DrawImage(top2Bm, top2P);
        //        g.DrawImage(top3Bm, top3P);
        //    }

        //    MyColorArray2 cl2 = new MyColorArray2(result.Width / scaleRatio, result.Height / scaleRatio);

        //    using (var writer = new System.IO.StreamWriter("test.csv"))
        //    {
        //        for (int h = 0; h < result.Height; h += scaleRatio)
        //            for (int w = 0; w < result.Width; w += scaleRatio)
        //            {

        //            }

        //        //var line = string.Format("{0},{1}", first, second);
        //        //writer.WriteLine(line);
        //        writer.Flush();
        //    }


        //    AppendLog(result.Width.ToString());

        //    return result;
        //}


        private MyUshortArray2 MergeArray2()
        {
            MyUshortArray2 top3a, bot3a, top2a, bot2a, top1a, bot1a, palma;

            bot1Bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
            top1Bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
            bot2Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
            top2Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
            bot3Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
            top3Bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
            palmBm.RotateFlip(RotateFlipType.Rotate180FlipY);

            bot1a = MyArrayAlgo.RotateArray2(bot1, MyArrayAlgo.RotateType.Rotate90);
            top1a = MyArrayAlgo.RotateArray2(top1, MyArrayAlgo.RotateType.Rotate90);
            bot2a = MyArrayAlgo.RotateArray2(bot2, MyArrayAlgo.RotateType.Rotate270);
            top2a = MyArrayAlgo.RotateArray2(top2, MyArrayAlgo.RotateType.Rotate270);
            bot3a = MyArrayAlgo.RotateArray2(bot3, MyArrayAlgo.RotateType.Rotate270);
            top3a = MyArrayAlgo.RotateArray2(top3, MyArrayAlgo.RotateType.Rotate270);
            //palma = MyArrayAlgo.RotateArray2(palm, MyArrayAlgo.RotateType.Rotate180);
            //palma = MyArrayAlgo.FlipArray2(palma, MyArrayAlgo.FlipType.FlipY);
            palma = MyArrayAlgo.FlipArray2(palm, MyArrayAlgo.FlipType.FlipY);

            int width = top3a.Width + bot3a.Width + palma.Width + bot1a.Width + top1a.Width + 4;
            int height = palma.Height;
            MyUshortArray2 arr2 = new MyUshortArray2(width, height, 3000);

            Point top3P = new Point(0, 0);
            Point bot3P = new Point(top3P.X + top3a.Width + 1, top3P.Y);
            Point palmP = new Point(bot3P.X + bot3a.Width + 1, bot3P.Y);
            Point top2P = new Point(0, palma.Height - top2a.Height);
            Point bot2P = new Point(top2P.X + top2a.Width + 1, top2P.Y);
            Point bot1P = new Point(palmP.X + palma.Width + 1, palma.Height / 2 - bot1a.Height / 2);
            Point top1P = new Point(bot1P.X + bot1a.Width + 1, bot1P.Y);

            arr2.WriteArray2(top3a, top3P);
            arr2.WriteArray2(top2a, top2P);
            arr2.WriteArray2(top1a, top1P);
            arr2.WriteArray2(bot3a, bot3P);
            arr2.WriteArray2(bot2a, bot2P);
            arr2.WriteArray2(bot1a, bot1P);
            arr2.WriteArray2(palma, palmP);

            return arr2;
        }
    }
}
