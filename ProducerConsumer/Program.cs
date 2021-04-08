using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new CommonData();
            var prods = new List<Producer>(3)
            {
                new Producer(data, 0),
                new Producer(data, 1),
                new Producer(data, 2)
            };
            var cons = new Consumer(data);
            cons.Start();
            foreach (var p in prods)
            {
                p.Start();
            }
            Thread.Sleep(15000);
            foreach (var p in prods)
            {
                p.Stop();
            }
            cons.Stop();
        }
    }
}
