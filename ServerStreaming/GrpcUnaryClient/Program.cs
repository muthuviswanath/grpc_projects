
using Grpc.Net.Client;

namespace GrpcUnaryclient
{
    public class Program
    {
       public static void Main(String[] args)
        {
            ServerStreamingDemo();
            Console.WriteLine("Press any key to exit ....");
            Console.ReadKey();
        }

        public static void ServerStreamingDemo() { 
        var channel = GrpcChannel.ForAddress("https://localhost:5001");
        var client = new StreamDe
        }
    }
}