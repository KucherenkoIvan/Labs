using System;
class Program
{
    const int N = 3; // Размер блока во Wrap()
    static void Main(string[] args)
    {
        int[] examples = new int[] { 1, 51, 963, 4444, 1342561231 }; //1, 51, 963, 448, 1135
        foreach (int i in examples)
            Console.WriteLine(Wrap(i));
        Console.ReadKey();
    }
    static int Wrap(int x)
    {
        int ret = 0;
        string s = x.ToString();
        while (s.Length % N != 0) // Дополняем до необходимой длины
            s = "0" + s;
        for (int i = 0; i < s.Length; i += N) // Суммируем блоки
            ret += int.Parse(s.Substring(i, N));
        return ret;
    }
}
