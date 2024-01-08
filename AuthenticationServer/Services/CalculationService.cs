using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace AuthenticationServer.Services
{

    public class CalculationService: Calculation.CalculationBase
    {
        [Authorize(Roles = "Administrator")]
        public override Task<CalculationResult> PerformAddition(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 + request.Number2 });
        }

        [Authorize(Roles = "User")]
        public override Task<CalculationResult> PerformSubtraction(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 - request.Number2 });
        }

        [Authorize(Roles = "Administrator,User")]
        public override Task<CalculationResult> PerformMultiplication(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 * request.Number2 });
        }

        [AllowAnonymous]
        public override Task<CalculationResult> PerformDivision(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 / request.Number2 });
        }

        [Authorize(Roles = "SuperUser")]
        public override Task<CalculationResult> PerformModulus(InputNumbers request, ServerCallContext context)
        {
            return Task.FromResult(new CalculationResult { Result = request.Number1 % request.Number2 });
        }
    }
}
