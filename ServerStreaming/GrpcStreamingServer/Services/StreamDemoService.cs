using Grpc.Core;
using GrpcStreamingServer;

namespace GrpcStreamingServer.Services
{
    public class StreamDemoService : StreamDemo.StreamDemoBase {

        public override async Task ServerStreamingDemo(Test request, 
            IServerStreamWriter<Test> responseStream, ServerCallContext context)
        {
            for (int i = 1; i <= 20; i++) {
                await responseStream.WriteAsync(new Test { TestMessage = $"{request.TestMessage} {i}" });
                await Task.Delay(1000);
            }
        }

    }
}
