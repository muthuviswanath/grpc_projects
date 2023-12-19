using Grpc.Core;

namespace GrpcService.Services
{
    public class SampleService : sample.sampleBase
    {
        public override Task<SampleResponse> GetFullName(SampleRequest request, ServerCallContext context)
        {
            var res = $"{request.Firstname} {request.Lastname}";
            return Task.FromResult( new SampleResponse { Fullname = res });
        }
    }
}
