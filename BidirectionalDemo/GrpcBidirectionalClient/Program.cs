using Grpc.Net.Client;
using GrpcBidirectionalServer;

namespace GrpcBidirectionalClient {
    public class Program { 
        public static async Task Main(string[] args)
        {

            await BidirectionalStreamingDemo();
        }

        public static async Task BidirectionalStreamingDemo() {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new StreamDemo.StreamDemoClient(channel);
            var stream = client.BidirectionalStreamingDemo();
            var requestTask = Task.Run(async () => {
                for (int i = 1; i <= 10; i++) {
                    await Task.Delay(1000);
                    await stream.RequestStream.WriteAsync(new MultipleTest { TestMessage = i.ToString() });
                    Console.WriteLine("Client Send Request: "+ i);
                }
                await stream.RequestStream.CompleteAsync();
            });
            var responseTask = Task.Run(async () => {
                while (await stream.ResponseStream.MoveNext(CancellationToken.None)) {
                    Console.WriteLine("Received Response: " + stream.ResponseStream.Current.TestMessage);
                }
                Console.WriteLine("Response Stream has got completed");
            
            });
            await Task.WhenAll(requestTask, responseTask);
        }
    }
}