namespace _03TasksWithCancellation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        static CancellationTokenSource Cts = new CancellationTokenSource();

        public static void SomeLongRunningTaskMethod()
        {
            while (true)
            {
                Console.WriteLine($"Some work in progress...");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                //if (Cts.Token.IsCancellationRequested)
                //{
                //    return;
                //}

                Cts.Token.ThrowIfCancellationRequested();
            }
        }

        private static void Main(string[] args)
        {
            Task t1 = new Task(SomeLongRunningTaskMethod, Cts.Token);
            Task t2 = new Task(SomeLongRunningTaskMethod, Cts.Token);
            Task t3 = new Task(SomeLongRunningTaskMethod, Cts.Token);
            Task t4 = new Task(SomeLongRunningTaskMethod, Cts.Token);

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Cts.Cancel();

            Console.WriteLine($"Main thread ...");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine($"Stop thread ...");
        }
    }
}
