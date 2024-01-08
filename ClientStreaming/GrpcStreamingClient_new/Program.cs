using Grpc.Net.Client;
using GrpcServer;

namespace GrpcStreamingClient_new {
    public class Program
    {

        public static async Task Main(string[] args)
        {
            await ClientStreamingDemo();
        }
        private static async Task ClientStreamingDemo()
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new StreamDemo.StreamDemoClient(channel);
            var stream = client.CientStreamingDemo();
            for (int i = 1; i <= 10; i++)
            {
                stream.RequestStream.WriteAsync(new Test { TestMessage = $"Welcome {i}" });
                await Task.Delay(1000);
            }
            await stream.RequestStream.CompleteAsync();
            Console.WriteLine("Client Streaming Completed Successfully");
        }
    }
}