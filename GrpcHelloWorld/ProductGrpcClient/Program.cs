using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ProductProtoService.ProductProtoServiceClient(channel);

            //GetProductAsync
            await GetProductAsync(client);

            //Get All Products
            await GetAllProductsAsync(client);

            // Add Products
            await AddProductAsync(client);

            //Get All Products after adding
            await GetAllProductsAsync(client);

        }

        private static async Task AddProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("Adding New products!");
            var addProductResponse = await client.AddProductAsync(
                new AddProductRequest {
                    Product = new ProductModel
                    { 
                    Name = "Laser Light",
                    Description = "RGB Light",
                    Price = 516,
                    Status = ProductStatus.Instock,
                    CreatedTime= Timestamp.FromDateTime(DateTime.UtcNow)
                    }
                }
                );
            Console.WriteLine($"Add Product Response: {addProductResponse.ToString()}");
            Console.ReadLine();
        }

        public static async Task GetProductAsync(ProductProtoService.ProductProtoServiceClient client) {
            Console.WriteLine("Getting details of a product!");
            var response = await client.GetProductAsync(
                new GetProductRequest
                {
                    ProductId = 2
                });
            Console.WriteLine($"Product Async Response: {response.ToString()}");
            Console.ReadLine();
        }

        public static async Task GetAllProductsAsync(ProductProtoService.ProductProtoServiceClient client) {
            Console.WriteLine("Getting details of all products!");
            using (var clientData = client.GetAllProducts(new GetAllProductsRequest()))
            {
                while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var currentProduct = clientData.ResponseStream.Current;
                    Console.WriteLine(currentProduct);
                }
            }
            Console.ReadLine();
        }
    }
}
