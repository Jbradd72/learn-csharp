using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CanvasApp1
{
    class CanvasApiFetcher
    {
        static readonly HttpClient client = new HttpClient();
        private static string BaseUri = "https://byui.instructure.com/api/v1";

        public CanvasApiFetcher()
        {
            client.DefaultRequestHeaders.Accept
               .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public void SetAuthToken(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<List<JObject>> GetResponseAsync(string endPoint)
        {
            Console.WriteLine($"Making request at {BaseUri}{endPoint}...");

            HttpResponseMessage response = await client.GetAsync($"{BaseUri}{endPoint}");
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error making get request.", e);
                throw;
            }

            dynamic data = null;
            List<JObject> list = new List<JObject>();
            try
            {
                data = JsonConvert.DeserializeObject(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error deserializing HTTP Response", e);
                throw;
            }

            if (data is JObject)
                list.Add(data);
            else
            {
                foreach (JObject job in data)
                    list.Add(job);
            }

            return list;
        }

    }
}
