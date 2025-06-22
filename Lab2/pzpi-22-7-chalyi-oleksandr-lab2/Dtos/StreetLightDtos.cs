using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record StreetlightCreateDto(
        string Location,
        string Type,
        string Status,
        int BrightnessLevel
    );

    public record StreetlightDto(
        int Id,
        string Location,
        string Type,
        DateTime InstallationDate,
        string Status,
        int BrightnessLevel,
        DateTime LastMaintenanceDate
    );

    public record StreetlightGetDto(
        int Id,
        string Location,
        string Type,
        DateTime InstallationDate,
        string Status,
        int BrightnessLevel,
        DateTime LastMaintenanceDate,
        IEnumerable<SensorGetDto>? Sensors
    );

    public record StreetlightGetAllDto(
        int Id,
        string Location,
        string Type,
        DateTime InstallationDate,
        string Status,
        int BrightnessLevel,
        DateTime LastMaintenanceDate
    );

    public record StreetlightUpdateDto(
        string? Location,
        string? Type,
        string? Status,
        int? BrightnessLevel,
        DateTime? LastMaintenanceDate
    );
}
