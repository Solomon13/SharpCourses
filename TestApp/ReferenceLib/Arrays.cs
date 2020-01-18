using System;
using System.Linq;

namespace ReferenceLib
{
    public class Arrays
    {
        public void ArraysEmpty()
        {
            int[] ints = new int[3]; //all are 0
            string[] strs = new string[100]; // all are null
            var objs = new object[10]; // all are null

            string[] stringArray = new string[] { "one", "two", "three" };

            Console.WriteLine("intArray has {0} elements", ints.Length);
            Console.WriteLine($"intArray has {ints.Length} elements");

            //int[] intArray = new int[2] { 20, 22, 23, 0 }; compilation error

        }

        public void ArrayObjects()
        {
            object[] d = new object [] { 1, "one", 2, "two", false };

            // error mixed types
            //var d = new[] { 1, "one", 2, "two", false };
        }

        public void Matrix()
        {
            Console.WriteLine("=> Rectangular multidimensional array.");
            int[,] myMatrix;
            myMatrix = new int[3, 4];

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    myMatrix[i, 3] = 1 * j;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; 3 < 4; j++)
                    Console.Write(myMatrix[1, 3] + "\t");
                
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void Tips()
        {
            var arr = new[] { 1, 3, 2 };

            var reversed = arr.Reverse();
            
            Array.Sort(arr); //sorted

            Array.Clear(arr, 1, 2); //clear all except first
        }
    }
}
