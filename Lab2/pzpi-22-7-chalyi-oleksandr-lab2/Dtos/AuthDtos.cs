namespace SmartLightSense.Dtos;

public record RegisterDto(
    string UserName,
    string Password,
    string? Email,
    string? PhoneNumber
);

public record LoginDto(
    string UserName,
    string Password
);
