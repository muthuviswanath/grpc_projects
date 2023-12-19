using Grpc.Core;
using GrpcServer;

namespace GrpcServer.Services
{
    public class StreamDemoServices: StreamDemo.StreamDemoBase
    {
        public override async Task<Test> CientStreamingDemo(IAsyncStreamReader<Test> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext()) {
                Console.WriteLine(requestStream.Current.TestMessage);
            }
            Console.WriteLine("Client Streamin has got completed .....");
            return new Test { TestMessage = "Streaming Over" };
        }
    }
}
