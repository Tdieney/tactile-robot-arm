using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAlgorithm
{
    public class MyUshortArray2
    {
        int height;
        int width;
        ushort[,] array2;
        ushort value = 0;

        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }
        public ushort[,] Array2 { get => array2; set => array2 = value; }
        public ushort Value { get => value; set => this.value = value; }

        public MyUshortArray2(int width, int height)
        {
            this.height = height;
            this.width = width;
            this.array2 = new ushort[height, width];
        }

        public MyUshortArray2(int width, int height, ushort valueInit)
        {
            this.height = height;
            this.width = width;
            this.array2 = new ushort[height, width];
            this.value = valueInit;

            for (int h = 0; h < height; h++)
                for (int w = 0; w < width; w++)
                    this.array2[h, w] = valueInit;
        }

        public void WriteArray2(MyUshortArray2 arr2, System.Drawing.Point p)
        {
            if (p.X + arr2.Width > this.Width || p.Y + arr2.height > this.height)
                throw new Exception("p.X or p.Y");

            for (int h = 0; h < arr2.Height; h++)
                for (int w = 0; w < arr2.Width; w++)
                    this.array2[h + p.Y, w + p.X] = arr2.Array2[h, w];
        }

        public void WriteArray2(MyUshortArray2 arr2, int x0, int y0)
        {
            if (x0 + arr2.Width > this.Width || y0 + arr2.height > this.height)
                throw new Exception("p.X or p.Y");

            for (int h = 0; h < arr2.Height; h++)
                for (int w = 0; w < arr2.Width; w++)
                    this.array2[h + y0, w + x0] = arr2.Array2[h, w];
        }


        public void WriteToCSVFile(string filePath, bool passed)
        {
            using (var writer = new System.IO.StreamWriter(filePath, true))
            {
                for (int h = 0; h < this.height; h++)
                {
                    for (int w = 0; w < this.width; w++)
                    {
                        ushort temp = this.array2[h, w];
                        string str;
                        if (temp != this.value)
                            str = string.Format("{0},", this.array2[h, w]);
                        else
                        {
                            if (passed)
                                str = "v,";
                            else
                                str = "x,";
                        }
                        writer.Write(str);
                    }
                    writer.WriteLine();
                }
                writer.WriteLine();
                writer.WriteLine();

                //var line = string.Format("{0},{1}", first, second);
                //writer.WriteLine(line);
                //writer.Flush();
            }
        }
    }
}
