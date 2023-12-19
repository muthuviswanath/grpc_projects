using Grpc.Net.Client;
using GrpcService;

namespace ConsoleApp
{
    public class Program { 
        public static async Task Main(string[] args) {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var  client = new sample.sampleClient(channel);
            var client2 = new Greeter.GreeterClient(channel);
            var client3 = new Product.ProductClient(channel);
            var response = client.GetFullName(new SampleRequest { Firstname = "Sachin", Lastname = "Tendulkar" });
            Console.WriteLine(response.Fullname);
            

            var result = client2.SayHello(new HelloRequest { Name = "Narendra Modi" });
            Console.WriteLine(result.Message);

            var prodresponse = await client3.saveProductsAsync(new ProductsModel
            {
                ProductName = "Applie Iphone 15",
                ProductDescription = "Latest IPhone",
                ProductPrice = 2342343
            });

            Console.WriteLine($"{prodresponse.StatusCode} | {prodresponse.IsSuccessful}");

            var prodlistresponse = await client3.GetProductsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (var prod in prodlistresponse.Products) {
                Console.WriteLine($"{prod.ProductName} | {prod.ProductDescription} | {prod.ProductPrice}");
            }
            Console.ReadKey();

        }
    }
}