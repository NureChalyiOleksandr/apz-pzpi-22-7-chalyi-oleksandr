using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IO;

namespace SmartLightSense.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreetlightController : ControllerBase
    {
        private readonly IStreetlightRepository _streetlightRepository;
        private readonly ISensorRepository _sensorRepository;

        public StreetlightController(IStreetlightRepository streetlightRepository, ISensorRepository sensorRepository)
        {
            _streetlightRepository = streetlightRepository;
            _sensorRepository = sensorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<StreetlightGetDto>>> GetAll()
        {
            var streetlights = await _streetlightRepository.GetAllAsync();

            var streetlightDtos = new List<StreetlightGetAllDto>();

            foreach (var streetlight in streetlights)
            {
                var streetlightDto = new StreetlightGetAllDto(
                    streetlight.Id,
                    streetlight.Location,
                    streetlight.Type,
                    streetlight.InstallationDate,
                    streetlight.Status,
                    streetlight.BrightnessLevel,
                    streetlight.LastMaintenanceDate
                );

                streetlightDtos.Add(streetlightDto);
            }

            return Ok(streetlightDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StreetlightDto>> GetById(int id)
        {
            var streetlight = await _streetlightRepository.GetByIdAsync(id);

            if (streetlight == null) return NotFound();

            var sensors = await _sensorRepository.GetByStreetLightIdAsync(id);

            var streetlightDto = new StreetlightGetDto(
                streetlight.Id,
                streetlight.Location,
                streetlight.Type,
                streetlight.InstallationDate,
                streetlight.Status,
                streetlight.BrightnessLevel,
                streetlight.LastMaintenanceDate,
                sensors.Select(sensor => new SensorGetDto(
                    sensor.Id, 
                    sensor.SensorType, 
                    sensor.InstallationDate, 
                    sensor.Status, 
                    sensor.Data, 
                    sensor.LastUpdate))
            );

            return Ok(streetlightDto);
        }

        [HttpPost]
        public async Task<ActionResult<StreetlightDto>> Create([FromBody] StreetlightCreateDto streetlightCreateDto)
        {
            var newStreetlight = new Streetlight
            {
                Location = streetlightCreateDto.Location,
                InstallationDate = DateTime.Now,
                Status = streetlightCreateDto.Status,
                Type = streetlightCreateDto.Type,
                BrightnessLevel = streetlightCreateDto.BrightnessLevel,
                LastMaintenanceDate = DateTime.UtcNow
            };

            var createdStreetlight = await _streetlightRepository.CreateAsync(newStreetlight);

            return Ok(createdStreetlight);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StreetlightDto>> Update(int id, [FromBody] StreetlightUpdateDto streetlightUpdateDto)
        {
            var existingStreetlight = await _streetlightRepository.GetByIdAsync(id);

            if (existingStreetlight == null) return NotFound();

            if (!string.IsNullOrEmpty(streetlightUpdateDto.Location))
            {
                existingStreetlight.Location = streetlightUpdateDto.Location;
            }

            if (!string.IsNullOrEmpty(streetlightUpdateDto.Status))
            {
                existingStreetlight.Status = streetlightUpdateDto.Status;
            }

            if (streetlightUpdateDto.BrightnessLevel != null)
            {
                existingStreetlight.BrightnessLevel = (int)streetlightUpdateDto.BrightnessLevel;
            }

            if (streetlightUpdateDto.LastMaintenanceDate != null)
            {
                existingStreetlight.LastMaintenanceDate = (DateTime)streetlightUpdateDto.LastMaintenanceDate;
            }

            var updatedStreetlight = await _streetlightRepository.UpdateAsync(existingStreetlight);

            var sensors = await _sensorRepository.GetByStreetLightIdAsync(id);

            var updatedStreetlightDto = new StreetlightGetAllDto(
                updatedStreetlight.Id,
                updatedStreetlight.Location,
                updatedStreetlight.Type,
                updatedStreetlight.InstallationDate,
                updatedStreetlight.Status,
                updatedStreetlight.BrightnessLevel,
                updatedStreetlight.LastMaintenanceDate
            );

            return Ok(updatedStreetlightDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _streetlightRepository.DeleteAsync(id);

            if (!success) return NotFound();

            return Ok();
        }
    }
}
