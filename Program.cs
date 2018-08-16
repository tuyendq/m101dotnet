using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace M101DotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            MainAsync(args).GetAwaiter().GetResult();
            System.Console.WriteLine();
            System.Console.WriteLine("Press Enter");
            System.Console.WriteLine();
        }

        static async Task MainAsync(string[] agrs)
        {
            // await Task.Delay(500);
            var client = new MongoClient();
            
        }
    }
}
