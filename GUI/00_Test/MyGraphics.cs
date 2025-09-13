using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyAlgorithm
{
    public static class MyGraphics
    {
        public static Bitmap colorBlend = CreateColorBlend(1, 0xFFF, LinearGradientMode.Vertical);
        public static Bitmap CreateColorBlend(int width, int height, LinearGradientMode mode)
        {
            Bitmap bm = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                // Draw with a vertical gradient.
                Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
                using (LinearGradientBrush br = new LinearGradientBrush(
                rect, Color.Red, Color.Blue, mode))
                {
                    // Define a rainbow blend.
                    ColorBlend color_blend = new ColorBlend();

                    //1
                    //color_blend.Colors = new Color[]
                    //            {
                    //                Color.Yellow,
                    //                Color.FromArgb(152, 211, 75),
                    //                Color.FromArgb(28, 184, 159),
                    //                Color.FromArgb(44, 141, 170),
                    //                Color.FromArgb(75, 77, 164), //blue
                    //                Color.DarkViolet
                    //            };
                    //color_blend.Positions = new float[]
                    //{
                    //    0,1/6f,3/6f,4/6f,5/6f,6/6f
                    //};

                    //2
                    color_blend.Colors = new Color[]
                                {
                                    //Color.FromArgb(251, 175, 63), //orange
                                    //Color.FromArgb(254, 221, 20),//yellow
                                    //Color.FromArgb(213, 222, 31), //light green
                                    //Color.FromArgb(36, 169, 225), //light blue
                                    //Color.FromArgb(75, 77, 164), //dark blue


                                    Color.FromArgb(75, 77, 164), //dark blue
                                    Color.FromArgb(36, 169, 225), //light blue
                                    Color.FromArgb(213, 222, 31), //light green
                                    Color.FromArgb(254, 221, 20),//yellow
                                    Color.FromArgb(251, 175, 63), //orange
                                };
                    color_blend.Positions = new float[]
                    {
                        //0, 0.5f/5f, 2/5f , 4.5f/5f , 5/5f
                        0, 1f/5f, 2f/5f , 4.5f/5f , 5/5f
                    };

                    //
                    br.InterpolationColors = color_blend;
                    // Fill a rectangle with the blended brush.
                    gr.FillRectangle(br, rect);
                }
            }

            return bm;
        }
        public static Color[] ConvertDataToColor(ushort[] data, ushort min, ushort max)
        {
            Color[] colorArr = new Color[data.Length];
            double scale = (double)(colorBlend.Height - 1) / (max - min);
            ushort d, temp;


            for (int i = 0; i < colorArr.Length; i++)
            {
                //ushort reverseData = (ushort)(colorBlend.Height - 1 - d);
                //colorArr[i] = colorBlend.GetPixel(0, reverseData);
                //temp = data[i];
                //if (temp - min < 0 || temp > max)
                //    temp = 0;
                //d = (ushort)((data[i] - min) * scale);

                temp = data[i];
                d = (ushort)((temp - (int)min) * scale);
                if (d < 0 || d > colorBlend.Height)
                    d = 0;

                colorArr[i] = colorBlend.GetPixel(0, d);
            }

            return colorArr;
        }

        public static MyColorArray2 ConvertDataToColor2(MyUshortArray2 data, ushort min, ushort max)
        {
            MyColorArray2 colorArray2 = new MyColorArray2(data.Width, data.Height);
            double scale = (double)(colorBlend.Height - 1) / (max - min);
            int d, temp;
            //ushort reverseData;

            try
            {
                for (int h = 0; h < colorArray2.Height; h++)
                {
                    for (int w = 0; w < colorArray2.Width; w++)
                    {
                        temp = data.Array2[h, w];
                        d = (ushort)((temp - (int)min) * scale);
                        if (d < 0 || d > colorBlend.Height)
                            d = 0;
                        //reverseData = (ushort)(colorBlend.Height - 1 - d);
                        colorArray2.Array2[h, w] = colorBlend.GetPixel(0, d);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("message from ConvertDataToColor2");
            }

            return colorArray2;
        }

        public static MyUshortArray2 ConvertColorToArray2(MyColorArray2 colorArr, ushort min, ushort max)
        {
            MyUshortArray2 arr2 = new MyUshortArray2(colorArr.Width, colorArr.Height);
            double scale = (double)(colorBlend.Height - 1) / (max - min);
            Color cl;

            for (int h = 0; h < colorArr.Height; h++)
                for (int w = 0; w < colorArr.Width; w++)
                {
                    cl = colorArr.Array2[h, w];

                }


            return arr2;
        }

        public static Bitmap SetPixelForBitmapMatrix(MyColorArray2 arr)
        {
            Bitmap bm = new Bitmap(arr.Width, arr.Height);

            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    bm.SetPixel(x, y, arr.Array2[y, x]);
                }
            }

            return bm;
        }

        private static MyColorArray2 GetPixelFromBitmapMatrix(Bitmap bm)
        {
            MyColorArray2 arr = new MyColorArray2(bm.Width, bm.Height);

            for (int h = 0; h < arr.Height; h++)
            {
                for (int w = 0; w < arr.Width; w++)
                {
                    arr.Array2[h, w] = bm.GetPixel(w, h);
                }
            }

            return arr;
        }

        public static Bitmap ScaleBitmap(Bitmap src, int ratio)
        {
            int newW = src.Width * ratio;
            int newH = src.Height * ratio;
            Bitmap bm = new Bitmap(newW, newH);
            MyColorArray2 colorArr = GetPixelFromBitmapMatrix(src);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                Color cl;
                Rectangle rect = new Rectangle(0, 0, ratio, ratio);
                using (SolidBrush br = new SolidBrush(Color.Black))
                {
                    for (int y = 0; y < newH; y += ratio)
                    {
                        for (int x = 0; x < newW; x += ratio)
                        {
                            cl = colorArr.Array2[y / ratio, x / ratio];
                            rect.X = x;
                            rect.Y = y;
                            br.Color = cl;
                            gr.FillRectangle(br, rect);
                        }
                    }
                }
            }

            return bm;
        }

        public static Bitmap GenerateMatrixBitmap(ushort[] data, int width, int height, ushort min, ushort max, int scaleRatio)
        {
            Color[] cl = MyGraphics.ConvertDataToColor(data, min, max);
            MyColorArray2 cl2 = MyArrayAlgo.ConvertArray1ToArray2(cl, width, height);
            Bitmap bm = MyGraphics.SetPixelForBitmapMatrix(cl2);
            bm = MyGraphics.ScaleBitmap(bm, scaleRatio);
            return bm;
        }

        public static Bitmap GenerateMatrixBitmap2(MyUshortArray2 data, ushort min, ushort max, int scaleRatio)
        {
            MyColorArray2 cl = ConvertDataToColor2(data, min, max);
            Bitmap bm = MyGraphics.SetPixelForBitmapMatrix(cl);
            bm = MyGraphics.ScaleBitmap(bm, scaleRatio);
            return bm;
        }


    }
}