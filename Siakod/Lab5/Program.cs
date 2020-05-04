using System;
using System.Diagnostics;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            Random r = new Random();
            Console.WriteLine("\n\n\n_______________________Linear vs barier:_______________________\n\n\n");
            for (int M = 5000; M <= 20000; M *= 2)
                for (int N = 1000; N <= 16000; N *= 2)
                {
                    Console.WriteLine($"\n\n{M} elements {N} times\n");
                    int[] arr = new int[M];
                    MakeData(ref arr);
                    s.Start();
                    for (int i = 0; i < N; i++)
                        LinearSearch(arr, r.Next());
                    s.Stop();
                    Console.WriteLine("Linear : " + s.Elapsed.TotalSeconds);
                    s.Reset();

                    s.Start();
                    for (int i = 0; i < N; i++)
                        BarierSearch(arr, r.Next());
                    s.Stop();
                    Console.WriteLine("Barier : " + s.Elapsed.TotalSeconds);
                    s.Reset();
                }

            Console.WriteLine($"\n\n\n_______________________Binary vs Interp:_______________________\n\n\n");
            for (int M = 5000; M <= 20000; M *= 2)
                for (int N = 1000; N <= 16000; N *= 2)
                {
                    Console.WriteLine($"\n\n{M} elements {N} times\n");
                    int[] arr = new int[M];
                    MakeData(ref arr);
                    Array.Sort(arr);

                    s.Start();
                    for (int i = 0; i < N; i++)
                        BinarySearch(arr, r.Next());
                    s.Stop();
                    Console.WriteLine("Binary : " + s.Elapsed.TotalSeconds);
                    s.Reset();

                    s.Start();
                    for (int i = 0; i < N; i++)
                        InterpSearch(arr, r.Next());
                    s.Stop();
                    Console.WriteLine("Interp : " + s.Elapsed.TotalSeconds);
                    s.Reset();
                }
            Console.WriteLine("\n\n\n###########################END###########################");
            Console.ReadKey();
        }
        static int InterpSearch(int[] array, int val)
        {
            Int64 mid, start = 0, end = array.Length - 1; //Эмпирическим путем выяснено, что точности Int32 не хватает, для корректного осуществления интерполяции, так что используем Int64
            while (array[start] < val && array[end] > val && array[end] != array[start])
            {
                mid = start + ((val - array[start]) * (end - start)) / (array[end] - array[start]);
                if (array[mid] < val)
                    start = mid + 1;
                else if (array[mid] > val)
                    end = mid - 1;
                else
                    return (int)mid;
            }
            if (array[start] == val)
                return (int)start;
            if (array[end] == val)
                return (int)end;
            return -1;
        }
        static void MakeData(ref int[] array)
        {
            Random r = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i] = r.Next();
        }
        static int BarierSearch(int[] array, int val)
        {
            if (array[array.Length - 1] == val)
                return array.Length - 1;
            array[array.Length - 1] = val;
            int i = 0;
            for (; array[i] != val; i++) ;
            return i;
        }
        static int LinearSearch(int[] array, int val)
        {
            int i = 0;
            for (; i < array.Length && array[i] != val; i++) ;
            if (i == array.Length) return -1;
            return i;
        }
        static int BinarySearch(int[] array, int val)
        {
            int start = 0, end = array.Length - 1, mid = (start + end) / 2;
            while (start < mid)
            {
                if (array[mid] < val)
                    start = mid;
                else
                    end = mid;
                mid = (start + end) / 2;
            }
            if (array[start] == val)
                return start;
            return -1;
        }
    }
}
