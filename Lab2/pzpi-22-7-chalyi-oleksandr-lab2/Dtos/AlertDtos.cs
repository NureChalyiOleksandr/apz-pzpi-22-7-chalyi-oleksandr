using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record AlertCreateDto(
        int? StreetlightId,
        int? SensorId,
        string AlertType
    );

    public record AlertDto(
        int Id,
        int? StreetlightId,
        int? SensorId,
        string AlertType,
        DateTime AlertDateTime,
        bool Resolved
    );

    public record AlertUpdateDto(
        int? StreetlightId,
        int? SensorId,
        string? AlertType,
        bool? Resolved
    );
}
