namespace _02ThreadsSumElementsV2
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

        private static void Main(string[] args)
        {
            Console.WriteLine("Start to calculate ...");

            var t1 = new Thread(Calculate);
            var p1 = new ThreadCalculateParam
            {
                StartFrom = 0,
                To = 2500
            };
            t1.Start(p1);

            var t2 = new Thread(Calculate);
            var p2 = new ThreadCalculateParam
            {
                StartFrom = 2501,
                To = 5000
            };
            t2.Start(p2);

            var t3 = new Thread(Calculate);
            var p3 = new ThreadCalculateParam
            {
                StartFrom = 5001,
                To = 7500
            };
            t3.Start(p3);

            var t4 = new Thread(Calculate);
            var p4 = new ThreadCalculateParam
            {
                StartFrom = 7501,
                To = 10000
            };
            t4.Start(p4);

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            Console.WriteLine($"Finish. Sum { p1.Result + p2.Result + p3.Result + p4.Result }");
        }

        // object, need a cast to our object
        public static void Calculate(object p)
        {
            var param = (ThreadCalculateParam)p;

            ulong sum = 0;

            for (ulong i = param.StartFrom; i < param.To; i++)
            {
                sum += i;
            }

            param.Result = sum;
        }

        public class ThreadCalculateParam
        {
            public ulong StartFrom { get; set; }

            public ulong To { get; set; }

            public ulong Result { get; set; }
        }
    }
}
