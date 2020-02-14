namespace _02ThreadsSumElements
{
    using System;
    using System.Threading;

    internal class Program
    {
        //private static void CalculateOld()
        //{
        //    Calculate(new ThreadCalculateParam
        //    {
        //        StartFrom = 0,
        //        To = 1000
        //    });
        //}

        // shared resource
        private static ulong sharedSum = 0;

        private static object someRefVariable = new object();

        private static void Main(string[] args)
        {
            Console.WriteLine("Start to calculate ...");

            var t1 = new Thread(Calculate);
            t1.Start(new ThreadCalculateParam
            {
                StartFrom = 0,
                To = 2500
            });

            var t2 = new Thread(Calculate);
            t2.Start(new ThreadCalculateParam
            {
                StartFrom = 2501,
                To = 5000
            });

            var t3 = new Thread(Calculate);
            t3.Start(new ThreadCalculateParam
            {
                StartFrom = 5001,
                To = 7500
            });

            var t4 = new Thread(Calculate);
            t4.Start(new ThreadCalculateParam
            {
                StartFrom = 7501,
                To = 10000
            });

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            
            Console.WriteLine($"Finish. Sum {sharedSum}");
        }

        // object, need a cast to our object
        public static void Calculate(object p)
        {
            var param = (ThreadCalculateParam) p;

            for (ulong i = param.StartFrom; i < param.To; i++)
            {
                // race condition
                //lock (someRefVariable)
                //{
                //    sharedSum += i;
                //}

                Monitor.Enter(someRefVariable);

                sharedSum += i;

                Monitor.Exit(someRefVariable);
            }

            //return sum;
        }

        public class ThreadCalculateParam
        {
            public ulong StartFrom { get; set; }

            public ulong To { get; set; }
        }
    }
}
