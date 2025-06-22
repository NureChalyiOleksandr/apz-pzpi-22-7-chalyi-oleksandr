namespace SmartLightSense.Dtos
{
    public record UserDto(
        int Id,
        string UserName,
        string Password,
        string Role,
        string? Email,
        string? PhoneNumber,
        DateTime LastLogin
    );

    public record UserUpdateDto(
        string? UserName,
        string? Password,
        string? Role,
        string? Email,
        string? PhoneNumber
    );
}
