namespace _01Threads
{
    using System;
    using System.Threading;

    internal class Program
    {
        private static void SomeMethod(object someInfo)
        {
            Console.WriteLine($"Start work in Thread [{Thread.CurrentThread.ManagedThreadId}]. Param: {someInfo}");

            // todo 
            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine($"Finish work in Thread [{Thread.CurrentThread.ManagedThreadId}].");
        }

        private static void Main(string[] args)
        {
            var thread1 = new Thread(SomeMethod);
            thread1.Start(12);

            var thread2 = new Thread(SomeMethod);
            thread2.Start(15);

            // main thread is blocked
            thread1.Join();

            // main thread is blocked
            thread2.Join();

            Console.WriteLine($"Hello from main Thread [{Thread.CurrentThread.ManagedThreadId}].");
        }
    }
}
