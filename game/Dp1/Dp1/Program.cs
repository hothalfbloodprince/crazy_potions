using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Program
    {
        public static int[] array = { -50, -150, 50, -10, 10, 20, -70 };

        public static void Main()
        {
            Console.WriteLine(CountSum(array));
        }

        public static int CountSum(int[] arr)
        {            
            int maxSum = arr[0];
            int currentSum = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                currentSum = Math.Max(arr[i], currentSum + arr[i]);
                maxSum = Math.Max(maxSum, currentSum);
            }
            return maxSum;
        }
    }
}