
using Grpc.Core;

namespace GrpcServiceApp2.Services
{
    public class DifferenceService: Difference.DifferenceBase
    {
        public override async Task<DifferenceResponse> Difference(DifferenceRequest request, ServerCallContext context)
        {
            var result = request.Number1 - request.Number2;
            await Task.Delay(10000);
            return new DifferenceResponse { Result = result };
        }
    }
}
