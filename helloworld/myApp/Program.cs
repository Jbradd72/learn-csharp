using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Net.Http.Headers;

namespace myApp
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            getResponseAsync().Wait();
        }
        

        static async Task getResponseAsync()
        {

            try
            {
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("http://www.somaku.com/users");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<JObject> obj = JsonConvert.DeserializeObject<List<JObject>>(responseBody);

                // Above three lines can be replaced with new helper method below
                JObject obj1 = obj[0];
                List<string> props = obj1.Properties().Select(prop => prop.Name).ToList();

                List<Dictionary<string, string>> csvOut = new List<Dictionary<string, string>>();


                using (StreamWriter writer = new StreamWriter("C:/Users/Jbradd/Documents/learn-csharp/helloworld/test.csv"))
                using (var csv = new CsvWriter(writer))
                {
                    foreach (string p in props)
                    {
                        csv.WriteField(p);
                    }
                    csv.NextRecord();

                    foreach (JObject job in obj)
                    {
                        foreach (string p in props)
                        {
                            try
                            {
                                string v = job.Property(p).Value.ToObject<string>();
                                csv.WriteField(v.Replace('\n', '\r'));
                            }
                            catch (ArgumentException e)
                            {
                                csv.WriteField(" ");
                            }
                        }
                        csv.NextRecord();
                    }
                }
                
            }  
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
        }
    }
}
