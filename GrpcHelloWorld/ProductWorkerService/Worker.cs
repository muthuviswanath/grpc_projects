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

namespace ProductWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly ProductFactory _factory;


        public Worker(ILogger<Worker> logger, IConfiguration config, ProductFactory factory)
        {
            _logger =logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Product Worker running at: {time} ", DateTimeOffset.Now);
                try {
                    using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                    var client = new ProductProtoService.ProductProtoServiceClient(channel);

                    _logger.LogInformation("Add the product thru worker service initialzed");
                    var addProductResponse = await client.AddProductAsync(await _factory.Generate());
                    _logger.LogInformation($"Adding Product Response: {addProductResponse.ToString()}");
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }
                await Task.Delay(_config.GetValue<int>("3000"), stoppingToken);
            }
        }
    }
}
