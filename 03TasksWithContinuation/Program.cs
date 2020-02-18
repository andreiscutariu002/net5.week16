namespace _03TasksWithContinuation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            //Task<int> t = new Task<int>(() =>
            //{
            //    Console.WriteLine("Hello from task");
            //    // todo calculation

            //    Thread.Sleep(TimeSpan.FromSeconds(2));

            //    return 10 + 10;
            //});

            //t.Start();
            //t.Wait();

            //Task t2 = new Task(Method, t.Result);
            //t2.Start();
            //t2.Wait();

            Task<int> t = new Task<int>(() =>
            {
                return CalculateSum();
            });

            var t2 = t.ContinueWith(prevTask =>
            {
                UsePrevSum(prevTask);
            });

            t2.ContinueWith(task =>
            {
                LatestWork();
            });

            t.Start();
            t.Wait();
        }

        private static void LatestWork()
        {
            Console.WriteLine($"Finish work. [{Thread.CurrentThread.ManagedThreadId}]");
        }

        private static void UsePrevSum(Task<int> prevTask)
        {
            Console.WriteLine($"Prev result: {prevTask.Result}. [{Thread.CurrentThread.ManagedThreadId}]");
        }

        private static int CalculateSum()
        {
            Console.WriteLine($"Hello from task with id. [{Thread.CurrentThread.ManagedThreadId}]");
            // todo calculation

            Thread.Sleep(TimeSpan.FromSeconds(5));

            return 10 + 10;
        }

        private static void Method(object o)
        {
            Console.WriteLine(o);
        }
    }
}
