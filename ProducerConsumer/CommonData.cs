using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    
    class CommonData
    {
        public static readonly int ProducerCount = 3;

        private Queue<int>[] values =
        {
            new Queue<int>(),
            new Queue<int>(),
            new Queue<int>()
        };

        public void AddValue(int index, int value)
        {
            index = Math.Abs(index) % ProducerCount;
            Monitor.Enter(values);
            values[index].Enqueue(value);
            Monitor.PulseAll(values);
            Monitor.Exit(values);
        }

        public int[] ConsumeValues()
        {
            var result = new int[ProducerCount];
            int i = 0;
            Monitor.Enter(values);
            foreach (var queue in values)
            {
                
                while (queue.Count == 0)
                {
                    Monitor.Wait(values);
                }
                result[i++] = queue.Dequeue();

            }
            Monitor.Exit(values);
            return result;
        }
    }
}
