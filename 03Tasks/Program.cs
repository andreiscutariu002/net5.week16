namespace _03Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        public static ulong Calculate(object p)
        {
            var param = (ThreadCalculateParam) p;

            ulong sum = 0;

            Console.WriteLine($"Work on thread with id: {Thread.CurrentThread.ManagedThreadId}.");

            for (var i = param.StartFrom; i < param.To; i++) sum += i;

            return sum;
        }

        private static void Main(string[] args)
        {
            // return types [2]
            var task1 = new Task<ulong>(Calculate, new ThreadCalculateParam
            {
                StartFrom = 0,
                To = 2500
            });

            var task2 = new Task<ulong>(Calculate, new ThreadCalculateParam
            {
                StartFrom = 2501,
                To = 5000
            });

            var task3 = new Task<ulong>(Calculate, new ThreadCalculateParam
            {
                StartFrom = 5001,
                To = 7500
            });

            var task4 = new Task<ulong>(Calculate, new ThreadCalculateParam
            {
                StartFrom = 7501,
                To = 10000
            });

            // handling exception [1]
            try
            {
                task1.Start();
                task2.Start();
                task3.Start();
                task4.Start();

                Task.WaitAll(task1, task2, task3, task4);

                Console.WriteLine($"Sum: {task1.Result + task2.Result + task3.Result + task4.Result}");
            }
            catch (AggregateException e)
            {
            }
        }

        public class ThreadCalculateParam
        {
            public ulong StartFrom { get; set; }

            public ulong To { get; set; }
        }
    }
}
