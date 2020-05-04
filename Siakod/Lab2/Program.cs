using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace SAoCDP_Lab2
{
    class Program
    {
        //определим метод для ввода целого неотрицательного числа (он нам еще пригодится)
        public static int Input(string message) //ну прямо как в питоне
        {
            int i;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out i))
                    if (i >= 0)
                        break;
                    else continue;
                Console.WriteLine("Ошибка! Повторите ввод");
                Console.ReadKey();
            }
            return i;
        }
        //Вычисление i-го числа Фибоначчи методом "Разделяй и властвуй"
        public static int RecursiveF(int index)
        {
            return (index == 0 ? 0 : index == 1 ? 1 : RecursiveF(index - 1) + RecursiveF(index - 2));
        }
        //Вычисление i-го числа Фибоначчи методом восходящего ДП
        public static int UpwardDPF(int index)
        {
            int[] arr = new int[index + 1];
            if (index <= 1)
                return index == 1 ? 1 : 0;
            arr[0] = 0; arr[1] = 1;
            for (int i = 2; i <= index; i++)
                arr[i] = arr[i - 1] + arr[i - 2];
            return arr[index];
        }
        //Вычисление i-го числа Фибоначчи методом нисходящего ДП
        public static int DownwardDPF(int index, ref int[] memory)
        {
            if (index <= 1)
                return index == 1 ? 1 : 0;
            else if (memory[index] != -1)
                return memory[index];
            return (memory[index] = DownwardDPF(index - 1, ref memory) + DownwardDPF(index - 2, ref memory));
        }

        //Вычисление i-го числа Фибоначчи с помощью формулы Бине
        public static int BineF(int index)
        {
            double g = (1 + Math.Sqrt(5)) / 2;
            return (int)((Math.Pow(g, index) - (Math.Pow(-g, -index))) / (2 * g - 1));
        }


        //Степень числа методом "Разделяй и властвуй"
        public static int Pow(int a, int N)
        {
            if (N == 0) return 1;
            if (N % 2 == 0)
                return (Pow(a, N / 2) * Pow(a, N / 2));
            else
                return (Pow(a, (N - 1) / 2) * Pow(a, (N - 1) / 2)) * a;
        }

        //Ханойские башни

        public static void SolveHanoi(int n, int i, int k)
        {
            
            if (n == 1)
                Console.WriteLine($"Move Disk 1 from {i} to {k}");
            else
            {
                int tmp = 6 - i - k;
                SolveHanoi(n - 1, i, tmp);
                Console.WriteLine($"Move Disk {n} from {i} to {k}");
                SolveHanoi(n - 1, tmp, k);
            }
        }


        //Черепашка

        //Данные

        static class Coord
        {
            public static int X;
            public static int Y;
            static Coord()
            {
                X = Y = 0;
            }
        }
        static string[][] Visual;

        /* Определим метод для чтения из файла
         * Будем получать квадратную матрицу, в качестве разделителей - пробелы*/
        public static int[][] GetBoard()
        {

            List<string[]> buffer = new List<string[]>();
            using (StreamReader r = new StreamReader("input.txt"))
            {
                while (!r.EndOfStream)
                    buffer.Add(r.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }

            int[][] Board = new int[buffer.Count][];
            Visual = new string[buffer.Count][];
            for (int i = 0; i < buffer.Count; i++)
            {
                Board[i] = new int[buffer[0].Length];

                Visual[i] = new string[buffer[0].Length];
                for (int j = 0; j < buffer.Count; j++)
                {
                    Board[i][j] = int.Parse(buffer[i][j]);

                    Visual[i][j] = Board[i][j].ToString();
                }
            }
            Visual[0][0] = ".";
            return Board;
        }

        //Метод для решения задачи
        public static int Turtle(int[][] board)
        {
            int ColumnSum = 0;
            if (Coord.Y == board.Length - 1 && Coord.X == board[0].Length - 1)
                return board[Coord.Y][Coord.X];
            for (int i = Coord.Y; i < board.Length; i++)
                ColumnSum += board[i][Coord.X];
            if (ColumnSum <= board[Coord.Y].Sum() && Coord.X + 1 < board[0].Length) //Тут черепаха решает пойти вправо или влево, в зависимости от того, что "сытнее": строка или столбец
            {
                Coord.X++;
            }
            else if (Coord.Y + 1 < board.Length)
            {
                Coord.Y++;
            }
            Visualize();
            return board[Coord.Y][Coord.X] + Turtle(board);
        }
        // Отрисовка черепашьей беготни
        public static void Visualize()
        {

            Console.Clear();
            Visual[Coord.Y][Coord.X] = "▓";
            string outp = "";
            foreach (string[] line in Visual)
            {
                foreach (string element in line)
                    outp += element + " ";
                outp += "\n";
            }
            Console.WriteLine(outp);
            Thread.Sleep(250);
            Visual[Coord.Y][Coord.X] = ".";

        }
        static void Main(string[] args)
        {
            
            //Задание 1 - работа с числами Фибонначи
            
            int index = Input("Введите индекс числа в последовательности Фибоначчи");

            int[] memory = new int[index + 1];
            for (int i = 0; i <= index; i++)
                memory[i] = -1;

            Stopwatch s = new Stopwatch();
            TimeSpan t;

            Console.WriteLine(">Вычисление методом \"Разделяй и властвуй\"");
            int res = 0;
            s.Start();
            //res = RecursiveF(index);
            s.Stop();
            t = s.Elapsed;
            Console.WriteLine("\tРезультат: " + res + "\n\tВремя: " + t.TotalMilliseconds + " ms");


            Console.WriteLine(">Вычисление методом восходящего дп");
            s.Reset();
            s.Start();
            res = UpwardDPF(index);
            s.Stop();
            t = s.Elapsed;
            Console.WriteLine("\tРезультат: " + res + "\n\tВремя: " + t.TotalMilliseconds + " ms");

            Console.WriteLine(">Вычисление методом нисходящего дп");
            s.Reset();
            s.Start();
            res = DownwardDPF(index, ref memory);
            s.Stop();
            t = s.Elapsed;
            Console.WriteLine("\tРезультат: " + res + "\n\tВремя: " + t.TotalMilliseconds + " ms");

            Console.WriteLine(">Вычисление по формуле Бине");
            s.Reset();
            s.Start();
            res = BineF(index);
            s.Stop();
            t = s.Elapsed;
            Console.WriteLine("\tРезультат: " + res + "\n\tВремя: " + t.TotalMilliseconds + " ms");
            Console.ReadKey();
            Console.Clear();


            //Задание 2 - Степень числа

            int a = Input("Введите основание");
            int N = Input("Введите показатель");
            Console.WriteLine(Pow(a, N));
            Console.ReadKey();
            Console.Clear();

            //Задание 3 - Ханойские башни
            SolveHanoi(4, 1, 3);
            Console.ReadKey();
            
            //Задание 4 - черепашка
            Console.WriteLine("Сумма составила: " + Turtle(GetBoard()));
            Console.ReadKey();
        }
    }
}
