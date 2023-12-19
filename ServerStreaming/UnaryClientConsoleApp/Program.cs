using Grpc.Net.Client;
using GrpcStreamingServer;

namespace UnaryClientConsoleApp
{
    public class Program {

        public static async Task Main(string[] args)
        {
            await ServerStreamingDemo();

            Console.WriteLine("Streaming Stopped");

        }


        private static async Task ServerStreamingDemo()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new StreamDemo.StreamDemoClient(channel);
            var response = client.ServerStreamingDemo(new Test { TestMessage = "Thank you" });
            while(await response.ResponseStream.MoveNext(CancellationToken.None))
            {
                var msg = response.ResponseStream.Current.TestMessage;
                Console.WriteLine(msg);

            }
            Console.WriteLine("Server Streaming Task has got completed....");
            await channel.ShutdownAsync();
        }


    }

    
}