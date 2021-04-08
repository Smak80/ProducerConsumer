using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Consumer
    {
        private CommonData Data
        {
            get;
            set;
        }

        private Thread t = null;
        private bool stop = false;


        public Consumer(CommonData data)
        {
            Data = data;
        }

        public void Start()
        {
            if (t?.IsAlive == true) return;
            stop = false;
            t = new Thread(new ThreadStart(consume));
            t.Start();
        }

        private void consume()
        {
            try
            {
                while (!stop)
                {
                    Console.WriteLine("Consumer: Ожидание данных");
                    var values = Data.ConsumeValues();
                    Console.WriteLine(values.Sum());
                }
            } catch { }
        }

        public void Stop()
        {
            stop = true;
            t.Interrupt();
        }
    }
}
