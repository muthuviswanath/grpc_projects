using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading;
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

            //Update Products
            await UpdateProductAsync(client);
            //Get All Products after updating
            await GetAllProductsAsync(client);

            //Delete Products
            await DeleteProductAsync(client);
            //Get All Products after deleting
            await GetAllProductsAsync(client);

            //InsertBulkProducts
            await InsertBulkProductsAsync(client);
            //Get All Products after bulk insert
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

        public static async Task UpdateProductAsync(ProductProtoService.ProductProtoServiceClient client) {
            Console.WriteLine("Updating a single product!");
            var updateProductResponse = await client.UpdateProductAsync(
                            new UpdateProductRequest { 
                                Product = new ProductModel { 
                                    ProductId = 1,
                                    Name = "Mobile",
                                    Description = "Smart Phone",
                                    Price = 56783,
                                    Status = ProductStatus.Instock,
                                    CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                                }
                            }
                );
            Console.WriteLine($"Update Product Response: {updateProductResponse.ToString()}");
            Thread.Sleep(1000);
            Console.ReadLine();
        }

        public static async Task DeleteProductAsync(ProductProtoService.ProductProtoServiceClient client) {
            Console.WriteLine("Deleting a single product!");
            var deleteresponse = await client.DeleteProductAsync(
                new DeleteProductRequest
                {
                    ProductId = 3
                });
            Console.WriteLine($"Product Delete Response: {deleteresponse.Success.ToString()}");
            Thread.Sleep(1000);
            Console.ReadLine();
        }

        public static async Task InsertBulkProductsAsync(ProductProtoService.ProductProtoServiceClient client) {
            Console.WriteLine("Inserting bulk products!");
            using var clientBulk = client.InsertBulkProduct();
            for (var i = 1; i <= 5; i++) {
                var productModel = new ProductModel
                {
                    Name = $"Product - {i}",
                    Description = $"Product {i} - Description",
                    Price = 3434 + i,
                    Status = ProductStatus.Instock,
                    CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                };
                await clientBulk.RequestStream.WriteAsync( productModel );
            }
            await clientBulk.RequestStream.CompleteAsync();

            var bulkInsertResponse = await clientBulk;
            Console.WriteLine($"Bulk Insert Status: {bulkInsertResponse.Success}. Insert Count: {bulkInsertResponse.InsertCount}");
            Thread.Sleep(1000);
        }
    }
}
