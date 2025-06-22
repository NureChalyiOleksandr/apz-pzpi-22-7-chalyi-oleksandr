using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record MaintenanceLogCreateDto(
        int? StreetlightId,
        int TechnicianId,
        int? AlertId,
        string IssueReported,
        string ActionTaken,
        string Status
    );

    public record MaintenanceLogDto(
        int Id,
        int? StreetlightId,
        int? AlertId,
        int TechnicianId,
        DateTime Date,
        string IssueReported,
        string ActionTaken,
        string Status
    );

    public record MaintenanceLogUpdateDto(
        int? StreetlightId,
        int? AlertId,
        int? TechnicianId,
        string? IssueReported,
        string? ActionTaken,
        string? Status
    );
}
