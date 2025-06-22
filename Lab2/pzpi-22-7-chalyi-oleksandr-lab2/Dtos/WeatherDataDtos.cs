using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record WeatherDataCreateDto(
        double Temperature,
        double Visibility,
        double Precipitation
    );

    public record WeatherDataDto(
        int Id,
        DateTime Date,
        double Temperature,
        double Visibility,
        double Precipitation
    );

    public record WeatherDataUpdateDto(
        double? Temperature,
        double? Visibility,
        double? Precipitation
    );
}
