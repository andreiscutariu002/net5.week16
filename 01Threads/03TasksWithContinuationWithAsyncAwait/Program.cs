namespace _03TasksWithContinuationWithAsyncAwait
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            RunMethod().Wait();

            Console.WriteLine($"Some info on main thread. [{Thread.CurrentThread.ManagedThreadId}]");
        }

        private static async Task RunMethod()
        {
            var r = await CalculateSum();

            await UsePrevSum(r);

            LatestWork();
        }

        private static void LatestWork()
        {
            Console.WriteLine($"Finish work. [{Thread.CurrentThread.ManagedThreadId}]");
        }

        private static Task UsePrevSum(int result)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Prev result: {result}. [{Thread.CurrentThread.ManagedThreadId}]");
            });
        }

        private static Task<int> CalculateSum()
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Hello from task with id. [{Thread.CurrentThread.ManagedThreadId}]");
                // todo calculation

                Thread.Sleep(TimeSpan.FromSeconds(5));

                return 10 + 10;
            });

            //return new Task<int>(() =>
            //{
            //    Console.WriteLine($"Hello from task with id. [{Thread.CurrentThread.ManagedThreadId}]");
            //    // todo calculation

            //    Thread.Sleep(TimeSpan.FromSeconds(5));

            //    return 10 + 10;
            //});
        }

        private static void Method(object o)
        {
            Console.WriteLine(o);
        }
    }
}
