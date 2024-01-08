using AuthenticationServer;
using Grpc.Core;
using Grpc.Net.Client;

namespace AuthenticationClient {

    public class Program { 
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5041");
            try {
                var authClient = new Authentication.AuthenticationClient(channel);
                var authResponse = authClient.Authenticate(new AuthenticationRequest
                {
                    UserName = "admin",
                    Password = "admin"
                });

                Console.WriteLine($"Recevied Auth Response | Token: {authResponse.AccessToken} | Expires In: {authResponse.ExpiresIn}");
                var calulate_client = new Calculation.CalculationClient(channel);
                var headers = new Metadata();
                headers.Add("Authorization", $"Bearer {authResponse.AccessToken}");

                var additionResult = calulate_client.PerformAddition(new InputNumbers { Number1 = 10, Number2 = 14 }, headers);
                Console.WriteLine($"Addition Result of (10 and 14):  {additionResult}  ");
            }
            catch (RpcException ex){
                Console.WriteLine($"Status Code: {ex.StatusCode} | Error: {ex.Message}");
                return;
            }
           
           

            
            await channel.ShutdownAsync();
        }
    }
}