using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab3
{
    class Program
    {
        static int M = 0, //количество очередей
            N = 0,//число клиентов
            C = 0;//число обслуженных клиентов
        static Random r = new Random();//для введение временных задержек
        static List<Queue<int>> que = new List<Queue<int>>();//список очередей

        static void Fill()//заполнение очередей
        {
            int clients = 0;
            while (N > 0)//Для N клиентов
            {
                int k = r.Next(0, que.Count);//выбираем случайную очередь
                Thread.Sleep(r.Next(2500, 3000));//ждем случайное время - моделирум реальные ситуации задержек
                que[k].Enqueue(clients++);//ставим в очередь
                Console.WriteLine(clients - 1 + " enqued in " + k);//мониторинг состояния
                N--; C++;
            }
        }

        static void Serve() //Удаление элементов из очереди
        {
            while (N != 0 || C != 0)//пока есть не вставшие в очередь или не обслуженные клиенты
            {
                int k = r.Next(0, que.Count);//выбираем случайную очередь
                if (que[k].Count == 0) //если она пуста - идем на след. итерацию
                    continue;
                Thread.Sleep(r.Next(3000, 6000));//моделируем задержку при обслуживании

                Console.WriteLine(que[k].Dequeue() + " served out in " + k);//вытаскиваем клиента из очереди + мониторинг состояния
                C--;
                if (que[k].Count == 0)
                    Console.WriteLine($"queue {k} is empty now");//проверка на число клиентов в очереди
            }
        }
        static void Main(string[] args)
        {
            
            Console.WriteLine("queues");
            M = int.Parse(Console.ReadLine()); //получаем от пользователя число очередей
            //fill ques
            for (int i = 0; i < M; i++)
                que.Add(new Queue<int>());

            Console.WriteLine("clients");
            N = int.Parse(Console.ReadLine()); // и число клиентов

            //параллельное выполнение избавит основной поток от блокировки при выполнении Sleep()
            //и добавит ситуацию, в которой клиенты могут появляться после начала обработки очередей
            new Thread(() => Fill()).Start();
            new Thread(() => Serve()).Start();
        }
    }
}
