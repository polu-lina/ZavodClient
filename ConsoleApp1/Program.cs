using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class ObjectDto
    {
        public Guid Id { get; set; }
        public double[] Position { get; set; }
        public int Rotation { get; set; }
        public string Type { get; set; }
    }

    class Program
    {
        //написать конструктор
        static HttpClient client = new HttpClient();
        static string BaseUrl = "objects";

        static async Task<Uri> CreateObjectAsync(ObjectDto objectDto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                BaseUrl, objectDto);
            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location;
        }

        static async Task<ObjectDto> GetObjectByIdAsync(Guid id)
        {
            ObjectDto objectDto = null;
            HttpResponseMessage response = await client.GetAsync(BaseUrl + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                //прописать ошибку и код,с которым завершился процесс
                objectDto = await response.Content.ReadAsAsync<ObjectDto>();
            }
            return objectDto;
        }

        static async Task<List<Guid>> GetObjectsIdAsync()
        {
            List<Guid> objectsDto = null;
            HttpResponseMessage response = await client.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                objectsDto = await response.Content.ReadAsAsync<List<Guid>>();
            }
            return objectsDto;
        }
    }

    /*
        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }
    

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                ObjectDto product = new ObjectDto
                {
                    Id = ;
                    Position = ;
                    Rotation =;
                    Type =;

                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                // Get the product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Price = 80;
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }
    }*/
}