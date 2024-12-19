using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    internal class HelperMethods
    {
        public static void ShowArray(int[] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }
        public static void ShowArray(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static bool AreEqual(int[] arr1, int[] arr2)
        {
            bool isEqual = true;
            for(int i = 0; i < arr1.GetLength(0); i++)
            {
                if (arr1[i] != arr2[i])
                {
                    isEqual = false;
                }
            }
            return isEqual;
        }
        public static int[] Add(int[] arr1, int[] arr2)
        {
            int length = arr1.GetLength(0);
            int[] arr = new int[length];
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                arr[i] = arr1[i] + arr2[i];
            }
            return arr;
        }
        public static int[] Subtract(int[] arr1, int[] arr2)
        {
            int length = arr1.GetLength(0);
            int[] arr = new int[length];
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                arr[i] = arr1[i] - arr2[i];
            }
            return arr;
        }
        public static double GetDistance(double[] pos1, double[]pos2)
        {
            return GetDistance(pos1[0], pos1[1], pos2[0], pos2[1]);
        }
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        public static double GetDistance(int[] pos1, int[]pos2)
        {
            return GetDistance(pos1[0], pos1[1], pos2[0], pos2[1]);
        }
        public static double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static void ShowList(List<int[]> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].GetLength(0); j++)
                {
                    Console.Write(list[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static int indexOf(List<List<int[]>> list, int[] arr)
        {
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                bool matches = true;
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (list[i].Last()[j] != arr[j])
                    {
                        matches = false;
                    }
                }
                if (matches)
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }
        public static int indexOf(List<List<int[]>> list1, List<int[]> list2)
        {
            int index = -1;
            for (int i = 0; i < list1.Count; i++)
            {
                bool matches = true;
                for (int j = 0; j < list2.Count(); j++)
                {
                    if(indexOf(list1[i], list2[j]) == -1)
                    {
                        matches = false;
                    }
                }
                if (matches)
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }
        public static int indexOf(List<int[]> list, int[] arr)
        {
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                bool matches = true;
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (list[i][j] != arr[j])
                    {
                        matches = false;
                    }
                }
                if(matches)
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }
    }
}
