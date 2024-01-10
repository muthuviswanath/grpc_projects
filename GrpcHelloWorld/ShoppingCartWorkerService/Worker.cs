using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductGrpc.Protos;
using ShoppingCartGrpc.Protos;
using IdentityModel.Client;
using System.Net.Http;
using Grpc.Core;

namespace ShoppingCartWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;



        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger =logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Product Worker running at: {time} ", DateTimeOffset.Now);

                // get the token from IS Version 4

                
                // Create the shoppin cart if not exist
                // REtrieve the products
                // Add the shopping cart items using stream
                try {
                    using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                    var client = new ProductProtoService.ProductProtoServiceClient(channel);

                    _logger.LogInformation("Add the product thru worker service initialzed");
                   
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }
                await Task.Delay(_config.GetValue<int>("3000"), stoppingToken);
            }
        }

        private async Task<string> GetTokenFromIS4() {
            _logger.LogInformation("Getting token from Identity Server...");

            var client = new HttpClient();
            var discount = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (discount.IsError) {
                _logger.LogError(discount.Error);
                return string.Empty;
            }
            _logger.LogInformation($"The data is from ID metadata. The end point is: {discount.TokenEndpoint}");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discount.TokenEndpoint,
                ClientId = "ShoppingCartClient",
                ClientSecret = "secret",
                Scope = "ShoppingCartAPI"
            });

            if(tokenResponse.IsError)
            {
                _logger.LogError(tokenResponse.Error);
                return string.Empty;
            }

            _logger.LogInformation($"Retireved token: {tokenResponse.AccessToken}");
            return tokenResponse.AccessToken;
        }

        private async Task<ShoppingCartModel> GetOrCreateShoppingCartAsync(ShoppingCartProtoService.ShoppingCartProtoServiceClient scClient, string token) {
            ShoppingCartModel shoppingCartModel;
            try
            {
                _logger.LogInformation("Shopping Cart App started...");
                var headers = new Metadata();
                headers.Add("Authorization", $"Bearer {token}");
                shoppingCartModel = await scClient.GetShoppingCartAsync(new GetShoppingCartRequest
                {
                    Username = "Muthu"
                }, headers);
                _logger.LogInformation($"Shopping cart response: {shoppingCartModel}");
            }
            catch (RpcException ex) {
                if (ex.StatusCode == StatusCode.NotFound)
                {
                    shoppingCartModel = await scClient.CreateShoppingCartAsync(new ShoppingCartModel
                    {
                        Username = "Muthu"
                    });

                }
                else { 
                    throw ex;
                }
            }
            return shoppingCartModel;
        }
    }
}
