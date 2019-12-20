using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Test.IP_Worker.Models;

namespace Test.IP_Worker {
    public class Worker : BackgroundService {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger) {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var messageBus = RabbitHutch.CreateBus("host=localhost");
                IPValidator valid = new IPValidator();
                messageBus.Subscribe<ResultIp>("SubscriptionId", msg => {
                    Console.WriteLine($"IP :{msg.IP} result {msg.Result}");
                });

                messageBus.Respond<ResultIp, Response>( request => new Response {
                    Status = true,
                    Error = "",
                    ResultIps = valid.ValidateIP(request)

                }) ;
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
