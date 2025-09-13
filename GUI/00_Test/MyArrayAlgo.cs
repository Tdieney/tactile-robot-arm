using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAlgorithm
{
    public static class MyArrayAlgo
    {
        public static void FiltUshortData(ushort[] src, ushort min, ushort max)
        {
            ushort temp;
            for (int i = 0; i < src.Length; i++)
            {
                temp = src[i];
                if (temp >= max)
                    src[i] = max;
                else if (temp <= min)
                    src[i] = min;
            }
        }

        public static ushort[] FiltAndCreateUshortArray(ushort[] src, ushort min, ushort max)
        {
            ushort[] arr = new ushort[src.Length];
            Array.Copy(src, arr, arr.Length);
            ushort temp;

            for (int i = 0; i < arr.Length; i++)
            {
                temp = arr[i];
                if (temp > max)
                    arr[i] = max;
                else if (temp < min)
                    arr[i] = min;
            }

            return arr;
        }

        public static ushort[] FiltData(ushort[] src, ushort min, ushort max)
        {
            List<ushort> list = new List<ushort>();
            ushort temp;

            for (int i = 0; i < src.Length; i++)
            {
                temp = src[i];
                if (temp > min)
                {
                    if (temp > max) src[i] = max;
                    list.Add(temp);
                }
            }

            return list.ToArray();
        }

        public static MyUshortArray2 ConvertArray1ToArray2(ushort[] arr1, int width, int height)
        {
            MyUshortArray2 arr2 = new MyUshortArray2(width, height);

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    arr2.Array2[h, w] = arr1[h * width + w];
                }
            }

            return arr2;
        }

        public static ushort[] ConvertArray2ToArray1(MyUshortArray2 arr2)
        {
            ushort[] arr1 = new ushort[arr2.Width * arr2.Height];

            for (int h = 0; h < arr2.Height; h++)
                for (int w = 0; w < arr2.Width; w++)
                    arr1[h * arr2.Width + w] = arr2.Array2[h, w];

            return arr1;
        }

        public static MyColorArray2 ConvertArray1ToArray2(Color[] src, int width, int height)
        {
            MyColorArray2 arr = new MyColorArray2(width, height);

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    arr.Array2[h, w] = src[h * width + w];
                }
            }

            return arr;
        }

        public static void DisplayArray2(MyUshortArray2 arr2)
        {
            Console.WriteLine();

            for (int h = 0; h < arr2.Height; h++)
            {
                for (int w = 0; w < arr2.Width; w++)
                {
                    string str = String.Format("{0,3} ", arr2.Array2[h, w]);
                    Console.Write(str);
                }
                Console.WriteLine();
            }
        }

        public static MyUshortArray2 SplitArray2(MyUshortArray2 src, int x0, int y0, int width, int height)
        {
            MyUshortArray2 arr2 = new MyUshortArray2(width, height);

            for (int h = 0; h < arr2.Height; h++)
            {
                for (int w = 0; w < arr2.Width; w++)
                {
                    arr2.Array2[h, w] = src.Array2[h + y0, w + x0];
                }
            }

            return arr2;
        }

        public static MyUshortArray2 MergeArray2(MyUshortArray2 arr2a, MyUshortArray2 arr2b)
        {
            int height, width;
            height = arr2a.Height;
            width = arr2a.Width + arr2b.Width;
            MyUshortArray2 arr2 = new MyUshortArray2(height, width);

            for (int h = 0; h < arr2.Height; h++)
            {
                for (int w = 0; w < arr2a.Width; w++)
                    arr2.Array2[h, w] = arr2a.Array2[h, w];
                for (int w = arr2a.Width; w < arr2.Width; w++)
                    arr2.Array2[h, w] = arr2b.Array2[h, w - arr2a.Width];
            }

            return arr2;
        }

        public static MyUshortArray2 RotateRightArray2(MyUshortArray2 src)
        {
            MyUshortArray2 arr2 = new MyUshortArray2(src.Width, src.Height);

            for (int h = 0; h < arr2.Height; h++)
            {
                for (int w = 0; w < arr2.Width; w++)
                {
                    arr2.Array2[h, w] = src.Array2[src.Height - 1 - w, h];
                }
            }

            return arr2;
        }

        public static ushort[] RotateRightArray1(ushort[] src, int width, int height)
        {
            MyUshortArray2 arr2 = ConvertArray1ToArray2(src, width, height);
            MyUshortArray2 arr2a = RotateRightArray2(arr2);
            ushort[] arr1 = ConvertArray2ToArray1(arr2a);

            return arr1;
        }

        public static bool CompareTwoArray2(MyUshortArray2 arr1, MyUshortArray2 arr2)
        {
            if (arr1.Height != arr2.Height)
                return false;
            if (arr1.Width != arr2.Width)
                return false;

            for (int w = 0; w < arr1.Width; w++)
                for (int h = 0; h < arr1.Height; h++)
                    if (arr1.Array2[h, w] != arr2.Array2[h, w])
                        return false;

            return true;
        }

        public static List<int> FindIndex1(byte[] arr, int numberOfBytesData, byte head, byte tail)
        {
            List<int> listIndx = new List<int>();
            int i = 0;

            while (i < arr.Length)
            {
                if ((arr[i] == head) && (arr[i + numberOfBytesData - 1] == tail))
                {
                    listIndx.Add(i);
                    i += numberOfBytesData;
                }
                else
                {
                    i++;
                }
            }

            return listIndx;
        }

        public static List<int> FindIndex2(byte[] arr, int size, byte head, byte tail)
        {
            if (arr == null)
                throw new Exception("input array is null");

            List<int> listIndx = new List<int>();
            int i = 0;
            int len = arr.Length - size;

            while (i < len)
            {
                if (arr[i] == head && arr[i + 1] == head)
                {
                    if (arr[i + size - 1] == tail && arr[i + size - 2] == tail)
                    {
                        listIndx.Add(i);
                        i += size;
                    }
                }
                else
                {
                    ++i;
                }
            }

            return listIndx;
        }

        //new
        public static List<int> FindIndex(byte[] arr, int size, byte head, byte tail)
        {
            if (arr == null)
                throw new Exception("input array is null");

            List<int> listIndxHead = new List<int>();
            List<int> listIndx = new List<int>();

            int len = arr.Length - size + 1;
            for (int i = 0; i < len; i++)
            {
                if ((arr[i] == head) && (arr[i + 1] == head))
                    listIndxHead.Add(i);
            }

            foreach (int indx in listIndxHead)
            {
                int nextIndx = indx + size - 2;
                if ((arr[nextIndx] == tail) && (arr[nextIndx + 1] == tail))
                    listIndx.Add(indx);
            }

            return listIndx;
        }

        public static ushort[] Average(ushort[][] arr, int len)
        {
            ushort[] average = new ushort[len];
            long[] sum = new long[len];

            for (int j = 0; j < len; j++)
                for (int i = 0; i < arr.Length; i++)
                    sum[j] += arr[i][j];

            for (int i = 0; i < len; i++)
                average[i] = (ushort)(sum[i] / arr.LongLength);

            return average;
        }

        public static void FiltArray(ushort[] src, ushort[] data)
        {
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] - data[i] <= 0)
                    src[i] = 0;
                else
                    src[i] -= data[i];
            }
        }

        //public static ushort[] FiltData(ushort[] src, ushort min, ushort max)
        //{
        //    List<ushort> list = new List<ushort>();
        //    ushort temp;

        //    for (int i = 0; i < src.Length; i++)
        //    {
        //        temp = src[i];
        //        if (temp > min)
        //        {
        //            if (temp > max) src[i] = max;
        //            list.Add(temp);
        //        }
        //    }

        //    return list.ToArray();
        //}

        public static ulong CalculateSum(MyUshortArray2 arr, ushort min, ushort max)
        {
            ulong sum = 0;
            ushort temp;

            try
            {
                for (int h = 0; h < arr.Height; h++)
                    for (int w = 0; w < arr.Width; w++)
                    {
                        temp = arr.Array2[h, w];
                        if (temp > min && temp < max)
                            sum += temp;
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("message from CalculateSum");
            }

            return sum;
        }



        //rotate array 2 dims
        public enum RotateType
        {
            Rotate90, Rotate180, Rotate270
        }

        public enum FlipType
        {
            FlipX, FlipY
        }

        public static MyUshortArray2 FlipArray2(MyUshortArray2 src, FlipType flipType)
        {
            MyUshortArray2 arr2 = new MyUshortArray2(src.Width, src.Height);

            switch (flipType)
            {
                case FlipType.FlipX:
                    for (int h = 0; h < arr2.Height; h++)
                        for (int w = 0; w < arr2.Width; w++)
                            arr2.Array2[h, w] = src.Array2[src.Height - h - 1, w];
                    break;
                case FlipType.FlipY:

                    for (int h = 0; h < arr2.Height; h++)
                        for (int w = 0; w < arr2.Width; w++)
                            arr2.Array2[h, w] = src.Array2[h, src.Width - w - 1];
                    break;
                default:
                    break;
            }

            return arr2;
        }

        public static MyUshortArray2 RotateArray2(MyUshortArray2 src, RotateType rotateType)
        {
            int height, width;
            MyUshortArray2 arr2 = new MyUshortArray2(1, 1);

            switch (rotateType)
            {
                case RotateType.Rotate90:
                    //transpose
                    height = src.Width;
                    width = src.Height;
                    arr2 = new MyUshortArray2(width, height);
                    //reverse each row
                    //for (int h = 0; h < arr2.Height; h++)
                    //    for (int w = 0; w < arr2.Width; w++)
                    //arr2.Array2[h, w] = src.Array2[src.Height - 1 - w, h];

                    for (int w = 0; w < src.Width; w++)
                        for (int h = src.Height - 1; h >= 0; h--)
                            arr2.Array2[w, src.Height - 1 - h] = src.Array2[h, w];
                    break;
                case RotateType.Rotate180:
                    //transpose
                    height = src.Height;
                    width = src.Width;
                    arr2 = new MyUshortArray2(width, height);
                    //
                    for (int h = 0; h < arr2.Height; h++)
                        for (int w = 0; w < arr2.Width; w++)
                            arr2.Array2[h, w] = src.Array2[src.Height - 1 - h, src.Width - 1 - w];
                    break;
                case RotateType.Rotate270:
                    //transpose
                    height = src.Width;
                    width = src.Height;
                    arr2 = new MyUshortArray2(width, height);
                    //
                    for (int w = src.Width - 1; w >= 0; w--)
                        for (int h = 0; h < src.Height; h++)
                            arr2.Array2[src.Width - 1 - w, h] = src.Array2[h, w];
                    break;
                default:
                    break;
            }

            return arr2;
        }


        public static ushort FindMax(MyUshortArray2 arr2)
        {
            ushort max = 0, temp;
            for (int h = 0; h < arr2.Height; h++)
                for (int w = 0; w < arr2.Width; w++)
                {
                    temp = arr2.Array2[h, w];
                    if (temp > max)
                        max = temp;
                }

            return max;
        }
    }
}
