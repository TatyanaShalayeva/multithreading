using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Timer2
{
    class Program
    {
        public static object locker = new object();
        static int circle = 1;

        static void Main(string[] args)
        {
            int num = 200;
            TimerCallback tm = new TimerCallback(Count);
            Timer timer = new Timer(tm, num, 0, 200);
            
            Console.ReadLine(); // почему, если убрать эту строчку, то ничего вообще не запускается?
        }
        public static void Count(object obj)
        {
            lock (locker)
            {
                int x = (int)obj;
                while (circle <= x)
                {
                    int z = circle * 100 / x;
                    Console.WriteLine($"Process conditions: {z}%, {Thread.CurrentContext.ContextID}");
                    Thread.Sleep(100);
                    circle++;
                }
            }

        }
   }
}

