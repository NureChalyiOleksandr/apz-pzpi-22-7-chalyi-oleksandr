using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record EnergyUsageCreateDto(
        int StreetlightId,
        double EnergyConsumed
    );

    public record EnergyUsageDto(
        int Id,
        int StreetlightId,
        DateTime Date,
        double EnergyConsumed
    );

    public record EnergyUsageUpdateDto(
        int? StreetlightId,
        double? EnergyConsumed
    );
}
