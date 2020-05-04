using System;
using System.IO;
using System.Linq;

namespace Lab4
{
    class Tree
    {
        public class Node
        {
            public Node L, R; //Левое и правое поддерево
            object value;
            public Node(object val)
            {
                value = val;
            }
            public static double PostOrder(Node n)
            {
                try
                {
                    if (double.TryParse(n.value.ToString(), out double v)) //Если встречено число - значит достигнут конец дерева
                        return v;
                    else
                    {
                        string op = n.value.ToString();
                        switch (op) //Иначе - элемент содержит знак операции, выполняем ее
                        {
                            case "+": return PostOrder(n.L) + PostOrder(n.R);
                            case "-": return PostOrder(n.L) - PostOrder(n.R);
                            case "*": return PostOrder(n.L) * PostOrder(n.R);
                            case "/": return PostOrder(n.L) / PostOrder(n.R);
                            default: return 0;
                        }
                    }
                }
                catch
                {
                    throw new Exception("Такое дерево не может существовать"); // В случае ошибки кидаем исключение
                }
            }
        }
        public Node root; //корень
        public double PostOrder() //постфиксный обход
        {
            return Node.PostOrder(root);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tree opTree = new Tree();
            //Тут начинаем заполнять дерево
            Tree.Node[] N = new Tree.Node[]
            {
                new Tree.Node(90),
                new Tree.Node("-"),
                new Tree.Node(18),
                new Tree.Node("/"),
                new Tree.Node(6),
                new Tree.Node("*"),
                new Tree.Node(4),
                new Tree.Node("+"),
                new Tree.Node(2),
            }; //(90 - 18) / (6 * (4 + 2))        --Примерно то, что должно получиться
            /*
             * {/}
             *      {-}
             *          {90}
             *          {18}
             *      {*}
             *          {6}
             *          {+}
             *              {4}
             *              {2}
             */
             //Заполняем массив
            opTree.root = N[3];
            opTree.root.L = N[1];
            opTree.root.L.L = N[0];
            opTree.root.L.R = N[2];
            opTree.root.R = N[5];
            opTree.root.R.L = N[4];
            opTree.root.R.R = N[7];
            opTree.root.R.R.L = N[6];
            opTree.root.R.R.R = N[8];
            //Вызываем метод решения
            Console.WriteLine(opTree.PostOrder());
            Console.ReadKey();
        }
    }
}
