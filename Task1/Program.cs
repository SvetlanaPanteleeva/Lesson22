using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArrey);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(SearchMax);
            Task task2 = task1.ContinueWith(action2);

            Action<Task<int[]>> action3 = new Action<Task<int[]>>(GetSum);

            Task task3 = task1.ContinueWith(action3);

            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArrey(object a)
        {   
            int n = (int)a;
            int[] arrey = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                arrey[i] = random.Next(0, 50);
                Console.Write("{0} ", arrey[i]);            
            }
            return arrey;              
        }
             
        static void SearchMax(Task<int[]> task)
        {
            int[] arrey=task.Result;
            int max = arrey[0];
            foreach (int a in arrey)
            {
                if (a > max)
                    max = a;
            }
            Console.WriteLine();
            Console.WriteLine("Значение максимального элемента {0}", max);
        }
        static void GetSum(Task<int[]> task)
        {
            int[] arrey = task.Result;
            int s = 0;
            for (int i = 0; i < arrey.Sum(); i++)
            {
                s++;
            }
            Console.WriteLine("Сумма чисел массива {0}", s);
        }
    }
}
