using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Producer
    {
        private CommonData Data
        {
            get;
            set;
        }

        private int index;
        public int Index
        {
            get => index;
            set => index = Math.Abs(value) % CommonData.ProducerCount;
        }

        private Thread t = null;
        private bool stop = false;
        
        private static Random r = new Random((int)DateTime.Now.Ticks);

        public Producer(CommonData data, int index)
        {
            Data = data;
            Index = index;
        }

        public void Start()
        {
            if (t?.IsAlive == true) return;
            stop = false;
            t = new Thread(new ThreadStart(produce));
        }

        private void produce()
        {
            while (!stop)
            {
                Thread.Sleep(r.Next(1000, 3000));
                var result = r.Next(0, 100);
                Data.AddValue(Index, result);
            }
        }

        public void Stop()
        {
            stop = true;
            t.Interrupt();
        }
    }
}
