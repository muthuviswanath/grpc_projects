using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceApp1;

namespace GrpcClientApp {
    public class Program { 
        public static async Task Main(string[] args)
        {
            var number1 = 100;
            var number2 = 50;

            Console.WriteLine("----------------------");
            Console.WriteLine($"Number 1: {number1}");
            Console.WriteLine($"Number 2: {number2}");
            Console.WriteLine("----------------------");

            var channel = GrpcChannel.ForAddress("http://localhost:5260");
            var calculationClient = new Calculation.CalculationClient(channel);
            await Sum(calculationClient, number1,number2);
            await Difference(calculationClient, number1, number2);

            await channel.ShutdownAsync();
            Console.ReadLine();

            
        }

        private static async Task Sum(Calculation.CalculationClient calculationClient, int number1, int number2) {
            try
            {
                var sumResult = await calculationClient.SumAsync(new CalcRequest
                {
                    Number1 = number1,
                    Number2 = number2
                }, deadline: DateTime.UtcNow.AddSeconds(25));

            }
            catch (RpcException ex) {
                if (ex.StatusCode == StatusCode.DeadlineExceeded) {
                    Console.WriteLine($"Result of Sum: Request Timed Out");
                }
            }
            
            
        }

        private static async Task Difference(Calculation.CalculationClient calculationClient, int number1, int number2) {

            try {

                var diffResult = await calculationClient.DifferenceAsync(new CalcRequest
                {
                    Number1 = number1,
                    Number2 = number2
                }, deadline: DateTime.UtcNow.AddSeconds(25));
                Console.WriteLine($"Result of Differnece: {diffResult.Result}");
            }

            catch (RpcException ex)
            {
                if (ex.StatusCode == StatusCode.DeadlineExceeded)
                {
                    Console.WriteLine($"Result of Differnece: Request Timed Out");
                }
            }
            
        }
    }
}