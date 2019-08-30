using System;
using System.Threading.Tasks;

namespace CanvasApp1
{
    class Program
    {
        
        private static string CANVAS_API_TOKEN = "CANVAS_API_TOKEN";
        static void Main(string[] args)
        {
           
            if (args.Length < 1)
            {
                Console.WriteLine("Please enter an endpoint to hit.");
                return;
            }
            else
            {
                MakeCanvasFetchAndWriteToCSVAsync(args[0]).Wait();
                Console.WriteLine("Task Completed.");
            }
            
        }

        static async Task MakeCanvasFetchAndWriteToCSVAsync(string endPoint)
        {
            CanvasApiFetcher caf = new CanvasApiFetcher();
            string token = Environment.GetEnvironmentVariable(CANVAS_API_TOKEN);
            if (token != null)
            {
                caf.SetAuthToken(token);
                CanvasCsvFileWriter.Write("C:/Users/Jbradd/Documents/learn-csharp/helloworld/test.csv", await caf.GetResponseAsync(endPoint));
            }
            else
            {
                Console.WriteLine("Hitting the canvas api requires an auth token as the environment variable 'CANVAS_API_TOKEN'");
                return;
            }

        }
        
    }
}
