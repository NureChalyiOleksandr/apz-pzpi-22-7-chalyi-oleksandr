using AutoMapper;
using SmartLightSense.Models;
using SmartLightSense.Dtos;

namespace SmartLightSense
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore());

            // MaintenanceLog mappings
            CreateMap<MaintenanceLog, MaintenanceLogDto>();
            CreateMap<MaintenanceLogCreateDto, MaintenanceLog>();
            CreateMap<MaintenanceLogUpdateDto, MaintenanceLog>();

            // Alert mappings
            CreateMap<Alert, AlertDto>();
            CreateMap<AlertCreateDto, Alert>()
                .ForMember(dest => dest.Resolved, opt => opt.Ignore());
            CreateMap<AlertUpdateDto, Alert>()
                .ForMember(dest => dest.StreetlightId, opt => opt.Ignore())
                .ForMember(dest => dest.SensorId, opt => opt.Ignore())
                .ForMember(dest => dest.AlertType, opt => opt.Ignore())
                .ForMember(dest => dest.AlertDateTime, opt => opt.Ignore());

            // Sensor mappings
            CreateMap<Sensor, SensorDto>();
            CreateMap<SensorCreateDto, Sensor>()
                .ForMember(dest => dest.LastUpdate, opt => opt.Ignore());
            CreateMap<SensorUpdateDto, Sensor>()
                .ForMember(dest => dest.LastUpdate, opt => opt.Ignore());

            // EnergyUsage mappings
            CreateMap<EnergyUsage, EnergyUsageDto>();
            CreateMap<EnergyUsageCreateDto, EnergyUsage>()
                .ForMember(dest => dest.Streetlight, opt => opt.Ignore());
            CreateMap<EnergyUsageUpdateDto, EnergyUsage>()
                .ForMember(dest => dest.Streetlight, opt => opt.Ignore());

            // Streetlight mappings
            CreateMap<Streetlight, StreetlightDto>();
            CreateMap<StreetlightCreateDto, Streetlight>()
                .ForMember(dest => dest.LastMaintenanceDate, opt => opt.Ignore());
            CreateMap<StreetlightUpdateDto, Streetlight>();

            // WeatherData mappings
            CreateMap<WeatherData, WeatherDataDto>();
            CreateMap<WeatherDataCreateDto, WeatherData>();
            CreateMap<WeatherDataUpdateDto, WeatherData>();
        }
    }
}
