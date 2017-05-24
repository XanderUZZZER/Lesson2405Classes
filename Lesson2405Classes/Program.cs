using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2405Classes
{
    class ArrayGenerator
    {
        private static readonly Random rand = new Random();
        int count { get; set; }
        int threadCount { get; set; }

        public ArrayGenerator(int count, int threadCount)
        {
            this.count = count;
            this.threadCount = threadCount;
        }
        public int[] Generate()
        {
            int[] arr = new int[count];
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threads.Length; i++)
            {
                int j = i;
                threads[i] = new Thread(() => GenerateInternal(j, arr));
            }
            foreach (var t in threads)
            {
                t.Start();
            }
            foreach (var t in threads)
            {
                t.Join();
            }
            return arr;
        }

        private void GenerateInternal(int threadIndex, int[] arr)
        {
            int start = arr.Length / threadCount * threadIndex;
            int end = //arr.Length / threadCount * (threadIndex +1);//
            threadIndex == threadCount - 1 ? arr.Length : arr.Length / threadCount * (threadIndex + 1);
            for (int i = start; i < end; i++)
            {
                arr[i] = rand.Next(0, 50);
            }
        }
    }

    class FrequencyCounter
    {
        int count { get; set; }
        int threadCount { get; set; }

        public FrequencyCounter(int count, int threadCount)
        {
            this.count = count;
            this.threadCount = threadCount;
        }
        public string Count(string text)
        {
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threads.Length; i++)
            {
                int j = i;
                threads[i] = new Thread(() => CountInternal(j, text));
            }
            foreach (var t in threads)
            {
                t.Start();
            }
            foreach (var t in threads)
            {
                t.Join();
            }
            return text;
        }

        private void CountInternal(int threadIndex, string text)
        {
            int start = text.Length / threadCount * threadIndex;
            int end = threadIndex == threadCount - 1 ? text.Length : text.Length / threadCount * (threadIndex + 1);
            //for (int i = start; i < end; i++)
                        
            var Mx = text.GroupBy(x => x).
                                OrderBy(g => g.Count()).
                                ToDictionary(p => p.Key, c => c.Count());
            
            
        }
    }

    class Incrementor
    {
        public Incrementor()
        {

        }

        public string Start()
        {
            var threads = new Thread[10];
            string s = "";
            Object syncObj = new Object();
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(() =>
                {
                    string sLocal = "";
                    for (int j = 0; j < 1000; j++)
                        sLocal += "o";

                    lock (syncObj) ;

                    s += sLocal;


                });
                //data++;
            }
            foreach (var item in threads)
                item.Start();
            foreach (var item in threads)
                item.Join();
            return s;
            //return data;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Lesson 2405 classes");

            //ArrayGenerator gen = new ArrayGenerator(200050, 3);
            //Console.WriteLine(string.Join(" ", gen.Generate()));
            //using (StreamWriter writer = new StreamWriter("1.txt"))
            //{
            //    foreach(var item in gen.Generate())
            //    {
            //        writer.Write(item + " ");
            //    }
            //}
            string [] readText = File.ReadAllLines(@"C:\Users\Dariya\Documents\Visual Studio 2015\Projects\Lesson2405Classes\Lesson2405Classes\bin\Debug\1.txt");
            var Mx = readText.SelectMany(y => y).GroupBy(x => x).
                                OrderByDescending(g => g.Count()).
                                ToDictionary(p => p.Key, c => c.Count());

            foreach (var item in Mx)
                Console.WriteLine($"{item.Key} -- \t{item.Value}");

            Thread t1 = new Thread(start1);
            Thread t2 = new Thread(start2);
            //t1.Start();
            //t2.Start();

            Console.ReadLine();
        }
        private static void start1()
        {
            Console.WriteLine("t1");
            Monitor.Enter(syncObj);
            try
            {
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine(1);
                    Thread.Sleep(150);
                }
            }
            finally
            {
                Monitor.Exit(syncObj);
            }

        }
        private static void start2()
        {
            Console.WriteLine("t2");
            Monitor.Enter(syncObj);
            try
            {
                for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(2);
                Thread.Sleep(150);
            }
            }
            finally
            {
                Monitor.Exit(syncObj);
            }
        }
        private static Object syncObj = new Object();
    }
}
