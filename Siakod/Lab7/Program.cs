using System;
using System.Linq;
using System.Diagnostics;
class Program
{
    static void Main()
    {
        Stopwatch s = new Stopwatch();
        int[] arr = default;
        int N = 5000;
        string[] states = new string[] { "Случайный набор", "Случайный набор", "Случайный набор", "Отсортирован", "Отсортирован + реверс" }; //для 20к+
        int stage = 0;
        for (int i = 0; i < 5; i++)
        {
            //Main info
            SimpleSorting.MakeData(ref arr, N, 1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n\n\n\n--- {N} элементов, {states[i]} ---\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            if (i >= 3)
                stage++;

            //Bubble
            {
                s.Start();
                SimpleSorting.Bubble(ref arr);
                s.Stop();
                Console.Write("\t-> Пузырьковая сортировка:            ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{s.Elapsed.TotalMilliseconds}мс\n");
                Console.ForegroundColor = ConsoleColor.White;
                SimpleSorting.MakeData(ref arr, N, 1000);
                s.Reset();
                if (stage == 1)
                    Array.Sort(arr);
                else if (stage == 2)
                {
                    Array.Sort(arr);
                    arr.Reverse();
                }
            }


            //Shaker
            {
                s.Start();
                SimpleSorting.Shaker(ref arr);
                s.Stop();
                Console.Write("\t-> Шейкер-сортировка:                 ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{s.Elapsed.TotalMilliseconds}мс\n");
                Console.ForegroundColor = ConsoleColor.White;
                SimpleSorting.MakeData(ref arr, N, 1000);
                s.Reset();
                if (stage == 1)
                    Array.Sort(arr);
                else if (stage == 2)
                {
                    Array.Sort(arr);
                    arr.Reverse();
                }
            }


            //Selection
            {
                s.Start();
                SimpleSorting.Selection(ref arr);
                s.Stop();
                Console.Write("\t-> Cортировка выбором:                ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{s.Elapsed.TotalMilliseconds}мс\n");
                Console.ForegroundColor = ConsoleColor.White;
                SimpleSorting.MakeData(ref arr, N, 1000);
                s.Reset();
                if (stage == 1)
                    Array.Sort(arr);
                else if (stage == 2)
                {
                    Array.Sort(arr);
                    arr.Reverse();
                }
            }


            //Insertion
            {
                s.Start();
                SimpleSorting.Insertion(ref arr);
                s.Stop();
                Console.Write("\t-> Cортировка вставками:              ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{s.Elapsed.TotalMilliseconds}мс\n");
                Console.ForegroundColor = ConsoleColor.White;
                SimpleSorting.MakeData(ref arr, N, 1000);
                s.Reset();
                if (stage == 1)
                    Array.Sort(arr);
                else if (stage == 2)
                {
                    Array.Sort(arr);
                    arr.Reverse();
                }
            }


            //BinaryInsertion
            {
                s.Start();
                SimpleSorting.BinaryInsertion(ref arr);
                s.Stop();
                Console.Write("\t-> Cортировка бинарными вставками:    ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{s.Elapsed.TotalMilliseconds}мс\n");
                Console.ForegroundColor = ConsoleColor.White;
                SimpleSorting.MakeData(ref arr, N, 1000);
                s.Reset();
                if (stage == 1)
                    Array.Sort(arr);
                else if (stage == 2)
                {
                    Array.Sort(arr);
                    arr.Reverse();
                }
            }

            if (N < 20000) N *= 2;

        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n\n\n\t[END]");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.ReadKey();
    }
}
