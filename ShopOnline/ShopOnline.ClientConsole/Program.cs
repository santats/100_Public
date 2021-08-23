using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ShopOnline.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri("https://localhost:44337");

            //1. without access token
            var resWithoutToken = client.GetAsync("/api/Product").Result;
            Console.WriteLine($"Result : {resWithoutToken.StatusCode}");

            //2. with access token (Product)
            client.DefaultRequestHeaders.Clear();
            Console.WriteLine("\nBegin Auth....");
            var jwt = GetJwt();
            Console.WriteLine($"\nToken={jwt}");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var product = client.GetAsync("/api/Product").Result;

            Console.WriteLine($"\nSend Request with token.");
            Console.WriteLine($"Result : {product.StatusCode}");
            Console.WriteLine(product.Content.ReadAsStringAsync().Result);

            //2. with access token (Customer)
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var customer = client.GetAsync("/api/Customer").Result;

            Console.WriteLine($"\nSend Request with token.");
            Console.WriteLine($"Result : {customer.StatusCode}");
            Console.WriteLine(customer.Content.ReadAsStringAsync().Result);

            //3. no auth service 
            Console.WriteLine("\nNo Auth Service Here ");
            client.DefaultRequestHeaders.Clear();
            var res = client.GetAsync("/api/Product/1").Result;
            Console.WriteLine($"Result : {res.StatusCode}");
            Console.WriteLine(res.Content.ReadAsStringAsync().Result);

            Console.Read();
        }


        private static string GetJwt()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44338");
            client.DefaultRequestHeaders.Clear();

            var res2 = client.GetAsync("/authentication?name=santosh&pwd=1234").Result;

            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);

            return jwt.access_token;
        }
    }
}
