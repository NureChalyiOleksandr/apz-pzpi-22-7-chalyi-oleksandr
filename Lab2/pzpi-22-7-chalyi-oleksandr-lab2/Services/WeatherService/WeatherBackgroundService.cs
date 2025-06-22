using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Services.WeatherService;
public class WeatherDataBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WeatherDataBackgroundService> _logger;

    public WeatherDataBackgroundService(IServiceProvider serviceProvider, ILogger<WeatherDataBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var weatherService = scope.ServiceProvider.GetRequiredService<WeatherService>();
                var weatherDataRepository = scope.ServiceProvider.GetRequiredService<IWeatherDataRepository>();

                try
                {
                    var weatherDataDto = await weatherService.GetWeatherForecastAsync("Kharkiv");
                    var weatherData = new WeatherData
                    {
                        Date = DateTime.Now,
                        Visibility = weatherDataDto.Visibility
                    };

                    await weatherDataRepository.CreateAsync(weatherData);
                    _logger.LogInformation("Weather data saved successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to save weather data");
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
