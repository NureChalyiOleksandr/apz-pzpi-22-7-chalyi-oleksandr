using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Services.EnergyUsageAlertBackgroundService
{
    public class EnergyUsageAlertBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly double _usageLimit = 50.0;

        public EnergyUsageAlertBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var streetlightRepository = scope.ServiceProvider.GetRequiredService<IStreetlightRepository>();
                        var energyUsageRepository = scope.ServiceProvider.GetRequiredService<IEnergyUsageRepository>();
                        var alertRepository = scope.ServiceProvider.GetRequiredService<IAlertRepository>();

                        var streetlights = await streetlightRepository.GetAllAsync();

                        foreach (var streetlight in streetlights)
                        {
                            var lastEnergyUsage = await energyUsageRepository.GetLatestByStreetlightIdAsync(streetlight.Id);

                            if (lastEnergyUsage == null)
                                continue;

                            var oneHourAgo = DateTime.Now.AddHours(-1);
                            var energyUsage = await energyUsageRepository.GetByDateAndStreetlightIdAsync(streetlight.Id, oneHourAgo);

                            if (energyUsage == null)
                                continue;

                            var energyUsed = energyUsage.EnergyConsumed;
                            var energyDifference = lastEnergyUsage.EnergyConsumed - energyUsed;

                            if (energyDifference > _usageLimit)
                            {
                                var existingAlerts = await alertRepository.GetByStreetLightIdAsync(streetlight.Id);
                                var existingAlert = existingAlerts
                                    .FirstOrDefault(alert => alert.AlertType == "EnergyUsageAlert" && alert.AlertDateTime >= oneHourAgo);

                                if (existingAlert == null)
                                {
                                    var message = $"Energy usage limit exceeded, " +
                                                  $"used: {energyDifference} watts, " +
                                                  $"allowed limit: {_usageLimit} watts.";

                                    var alert = new Alert
                                    {
                                        StreetlightId = streetlight.Id,
                                        SensorId = null,
                                        AlertType = "EnergyUsageAlert",
                                        Message = message,
                                        AlertDateTime = DateTime.Now,
                                        Resolved = false
                                    };
                                    await alertRepository.CreateAsync(alert);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while checking energy usage alerts: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}