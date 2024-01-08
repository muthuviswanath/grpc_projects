using Grpc.Core;
using GrpcBidirectionalServer;

namespace GrpcBidirectionalServer.Services
{
    public class StreamDemoService: StreamDemo.StreamDemoBase
    {
        public override async Task BidirectionalStreamingDemo(IAsyncStreamReader<MultipleTest> requestStream, IServerStreamWriter<MultipleTest> responseStream, ServerCallContext context)
        {
            var tasks = new List<Task>();
            while (await requestStream.MoveNext()) {
                Console.WriteLine("Received Request: " + requestStream.Current.TestMessage);
                var task = Task.Run(async () =>
                {
                    var message = requestStream.Current.TestMessage;
                    await Task.Delay(1000);
                    await responseStream.WriteAsync(new MultipleTest { TestMessage = message });
                    Console.WriteLine("Sent Response: " + message);
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Bi-directional Streaming is completed");
        }
    }
}
