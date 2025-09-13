using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAlgorithm
{
    public class MyColorArray2
    {
        int height;
        int width;
        Color[,] array2;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public Color[,] Array2 { get => array2; set => array2 = value; }

        public MyColorArray2(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.array2 = new Color[height, width];
        }
    }
}