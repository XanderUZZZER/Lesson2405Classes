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

            for (int i = 0; i< threads.Length; i++)
            {
                int j = i;
                threads[i] = new Thread(() => GenerateInternal(j, arr));
            }
            foreach(var t in threads)
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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lesson 2405 classes");

            ArrayGenerator gen = new ArrayGenerator(200050, 3);
            Console.WriteLine(string.Join(" ", gen.Generate()));
            using (StreamWriter writer = new StreamWriter("1.txt"))
            {
                foreach(var item in gen.Generate())
                {
                    writer.Write(item + " ");
                }
            }

            Console.ReadLine();
        }
    }
}
