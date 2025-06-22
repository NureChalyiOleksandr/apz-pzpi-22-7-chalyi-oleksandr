using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;
using AutoMapper;

namespace SmartLightSense.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISensorRepository _sensorRepository;
        private readonly IStreetlightRepository _streetLightRepository;

        public SensorController(ISensorRepository sensorRepository, IStreetlightRepository streetlightRepository, IMapper mapper)
        {
            _mapper = mapper;
            _sensorRepository = sensorRepository;
            _streetLightRepository = streetlightRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSensor([FromBody] SensorCreateDto sensorCreateDto)
        {
            if (sensorCreateDto == null) return BadRequest();

            var streetLight = await _streetLightRepository.GetByIdAsync(sensorCreateDto.StreetlightId);
            var sensor = new Sensor
            {
                SensorType = sensorCreateDto.SensorType,
                InstallationDate = DateTime.Now,
                Status = sensorCreateDto.Status,
                Data = sensorCreateDto.Data,
                StreetlightId = sensorCreateDto.StreetlightId,
                LastUpdate = DateTime.UtcNow,
                Streetlight = streetLight
            };

            var createdSensor = await _sensorRepository.CreateAsync(sensor);

            var result = new SensorDto(
                createdSensor.Id,
                createdSensor.SensorType,
                createdSensor.InstallationDate,
                createdSensor.Status,
                createdSensor.Data,
                createdSensor.LastUpdate,
                createdSensor.StreetlightId
            );

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, [FromBody] SensorUpdateDto sensorUpdateDto)
        {
            if (sensorUpdateDto == null) return BadRequest();

            var existingSensor = await _sensorRepository.GetByIdAsync(id);
            if (existingSensor == null) return NotFound();

            if (sensorUpdateDto.SensorType != null)
            {
                existingSensor.SensorType = sensorUpdateDto.SensorType;
            }

            if (sensorUpdateDto.Status != null)
            {
                existingSensor.Status = sensorUpdateDto.Status;
            }

            if (sensorUpdateDto.Data != null)
            {
                existingSensor.Data = sensorUpdateDto.Data;
            }

            if (sensorUpdateDto.StreetlightId != null)
            {
                existingSensor.StreetlightId = (int)sensorUpdateDto.StreetlightId;
                var streetLight = await _streetLightRepository.GetByIdAsync((int)sensorUpdateDto.StreetlightId);
                existingSensor.Streetlight = streetLight;
            }

            existingSensor.LastUpdate = DateTime.UtcNow;

            await _sensorRepository.UpdateAsync(existingSensor);

            var updatedSensorDto = new SensorDto(
                existingSensor.Id,
                existingSensor.SensorType,
                existingSensor.InstallationDate,
                existingSensor.Status,
                existingSensor.Data,
                existingSensor.LastUpdate,
                existingSensor.StreetlightId
            );

            return Ok(updatedSensorDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null) return NotFound();

            var result = await _sensorRepository.DeleteAsync(id);
            if (!result) return StatusCode(500, "An error occurred while deleting the sensor.");

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorById(int id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null) return NotFound();

            var sensorDto = new SensorDto(
                sensor.Id,
                sensor.SensorType,
                sensor.InstallationDate,
                sensor.Status,
                sensor.Data,
                sensor.LastUpdate,
                sensor.StreetlightId
            );

            return Ok(sensorDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSensors()
        {
            var sensors = await _sensorRepository.GetAllAsync();

            var sensorDtos = sensors.Select(sensor => new SensorDto(
                sensor.Id,
                sensor.SensorType,
                sensor.InstallationDate,
                sensor.Status,
                sensor.Data,
                sensor.LastUpdate,
                sensor.StreetlightId
            )).ToList();

            return Ok(sensorDtos);
        }

    }
}
