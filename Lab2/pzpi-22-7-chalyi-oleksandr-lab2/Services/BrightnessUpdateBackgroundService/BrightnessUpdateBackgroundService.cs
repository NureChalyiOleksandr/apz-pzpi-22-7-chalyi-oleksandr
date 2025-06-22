using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Services.BrightnessUpdateBackgroundService
{
    public class BrightnessUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BrightnessUpdateBackgroundService(IServiceScopeFactory scopeFactory)
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
                        var sensorRepository = scope.ServiceProvider.GetRequiredService<ISensorRepository>();
                        var weatherDataRepository = scope.ServiceProvider.GetRequiredService<IWeatherDataRepository>();

                        var streetlights = await streetlightRepository.GetAllAsync();
                        var weatherData = await weatherDataRepository.GetLatestWeatherAsync();

                        if (weatherData == null)
                        {
                            weatherData = new WeatherData { Visibility = 1.0 };
                        }

                        foreach (var streetlight in streetlights)
                        {
                            var streetlightsInSector = await streetlightRepository.GetBySectorIdAsync(streetlight.SectorId);
                            var sensorsInSector = new List<Sensor>();
                            foreach (var streetlightInSector in streetlightsInSector)
                            {
                                var sensorsForStreetlight = await sensorRepository.GetByStreetLightIdAsync(streetlightInSector.Id);
                                sensorsInSector.AddRange(sensorsForStreetlight);
                            }

                            var motionSensor = sensorsInSector.FirstOrDefault(sensor => sensor.SensorType == "Motion" && sensor.Data == 1);
                            var motionDetected = motionSensor != null;

                            var lightSensor = sensorsInSector.FirstOrDefault(sensor => sensor.SensorType == "Light" && sensor.StreetlightId == streetlight.Id);
                            var lightIntensity = lightSensor != null ? lightSensor.Data : 0;

                            int brightness = CalculateBrightness(lightIntensity, motionDetected, weatherData.Visibility);

                            await streetlightRepository.UpdateBrightnessAsync(streetlight.Id, brightness);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating brightness: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        private int CalculateBrightness(int lightIntensity, bool motionDetected, double visibility)
        {
            int normalLightIntensity = 1000;
            double visibilityFactor = visibility < 0.05 ? 1.5 : (visibility > 0.5 ? 1 : 1.5 - (visibility - 0.05) * (0.5 - 1) / (0.5 - 0.05));
            double lightIntensityFactor = lightIntensity < normalLightIntensity ? 1 + (normalLightIntensity - lightIntensity) / (double)normalLightIntensity : 1;

            if (visibility >= 0.2 && lightIntensity >= normalLightIntensity)
                return 0;

            var brightness = visibilityFactor * lightIntensityFactor * 50;
            return motionDetected ? (int)(brightness * 1.5) : (int)(brightness);
        }
    }
}
