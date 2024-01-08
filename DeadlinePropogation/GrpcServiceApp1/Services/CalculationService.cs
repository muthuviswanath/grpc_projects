using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceApp2;

namespace GrpcServiceApp1.Services
{
    public class CalculationService : Calculation.CalculationBase
    {
        public override async Task<CalcResponse> Sum(CalcRequest request, ServerCallContext context)
        {
            var result = request.Number1 + request.Number2;
            await Task.Delay(10000);
            return new CalcResponse { Result = result };
        }

        public override async Task<CalcResponse> Difference(CalcRequest request, ServerCallContext context)
       {
            var channel = GrpcChannel.ForAddress("http://localhost:5173");
            var diffClient = new Difference.DifferenceClient(channel);
            var diffResponse = await diffClient.DifferenceAsync(new DifferenceRequest
            {
                Number1 = request.Number1,
                Number2 = request.Number2,
            },deadline:context.Deadline);
            await channel.ShutdownAsync();
            return new CalcResponse { Result = diffResponse.Result };
        }

    }
}
