using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Timer3
{
    class Program
    {
        public static AutoResetEvent waitHandler = new AutoResetEvent(false);
        static int circle = 1;
        static void Main(string[] args)
        {
            TimerCallback tm = new TimerCallback(Count);
            Timer timer = new Timer(tm, waitHandler, 0, 50);
            waitHandler.WaitOne();
            timer.Dispose();
        }

        public static void Count(object obj)
        {
            AutoResetEvent x = (AutoResetEvent)obj;
            while (circle <= 100)
            {
                Console.WriteLine($"Process conditions: {circle}%, {Thread.CurrentContext.ContextID}");
                Thread.Sleep(200);
                circle++;
                if (circle == 100)
                    waitHandler.Set();
            }

        }
    }
}
