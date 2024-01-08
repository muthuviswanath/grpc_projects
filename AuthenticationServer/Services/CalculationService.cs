using Grpc.Core;

namespace AuthenticationServer.Services
{
    public class CalculationService: Calculation.CalculationBase
    {
        public override Task<CalculationResult> PerformAddition(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 + request.Number2 });
        }

        public override Task<CalculationResult> PerformSubtraction(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 - request.Number2 });
        }

        public override Task<CalculationResult> PerformMultiplication(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 * request.Number2 });
        }

        public override Task<CalculationResult> PerformDivision(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 / request.Number2 });
        }

        public override Task<CalculationResult> PerformModulus(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 % request.Number2 });
        }
    }
}
