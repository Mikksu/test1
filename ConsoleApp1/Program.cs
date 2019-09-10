using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log.Info("Test info.");

            ThreadPool.SetMinThreads(150, 150);

            Task.Run(async () =>
            {
                try
                {
                    await Task.WhenAll(new Task[] 
                    {
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                        AsyncFoo(),
                    });
                }
                catch(Exception ex)
                {
                    //log.Error("Error occurred while running AsyncFoo().", ex);
                }
            });

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


        static async Task AsyncFoo()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    Task.Delay(5);

                    log.Error("user-unhandled error.", new Exception($"exception loop {i}"));

                    //throw new Exception($"exception loop {i}");
                }
            });
        }
    }
}
