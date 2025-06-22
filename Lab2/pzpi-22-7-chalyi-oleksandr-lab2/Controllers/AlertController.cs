using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Dtos;
using SmartLightSense.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmartLightSense.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IStreetlightRepository _streetlightRepository;
        private readonly ISensorRepository _sensorRepository;

        public AlertController(IAlertRepository alertRepository, IStreetlightRepository streetlightRepository, ISensorRepository sensorRepository)
        {
            _alertRepository = alertRepository;
            _streetlightRepository = streetlightRepository;
            _sensorRepository = sensorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] AlertCreateDto alertCreateDto)
        {
            if (alertCreateDto == null)
                return BadRequest("Invalid alert data");

            var newAlert = new Alert
            {
                StreetlightId = alertCreateDto.StreetlightId,
                SensorId = alertCreateDto.SensorId,
                AlertType = alertCreateDto.AlertType,
                AlertDateTime = DateTime.Now,
                Resolved = false
            };

            if (alertCreateDto.StreetlightId != null)
            {
                var streetLight = await _streetlightRepository.GetByIdAsync((int)alertCreateDto.StreetlightId);
                newAlert.Streetlight = streetLight;
            }

            if (alertCreateDto.SensorId != null)
            {
                var sensor = await _sensorRepository.GetByIdAsync((int)alertCreateDto.SensorId);
                newAlert.Sensor = sensor;
            }

            var createdAlert = await _alertRepository.CreateAsync(newAlert);

            var alertDto = new AlertDto(
                createdAlert.Id,
                createdAlert.StreetlightId,
                createdAlert.SensorId,
                createdAlert.AlertType,
                createdAlert.AlertDateTime,
                createdAlert.Resolved
            );

            return CreatedAtAction(nameof(GetAlertById), new { id = createdAlert.Id }, alertDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertById(int id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null)
                return NotFound("Alert not found");

            var alertDto = new AlertDto(
                alert.Id,
                alert.StreetlightId,
                alert.SensorId,
                alert.AlertType,
                alert.AlertDateTime,
                alert.Resolved
            );

            return Ok(alertDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlerts()
        {
            var alerts = await _alertRepository.GetAllAsync();

            var alertDtos = alerts.Select(alert => new AlertDto(
                alert.Id,
                alert.StreetlightId,
                alert.SensorId,
                alert.AlertType,
                alert.AlertDateTime,
                alert.Resolved
            )).ToList();

            return Ok(alertDtos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlert(int id, [FromBody] AlertUpdateDto alertUpdateDto)
        {
            if (alertUpdateDto == null)
                return BadRequest("Invalid alert data");

            var existingAlert = await _alertRepository.GetByIdAsync(id);
            if (existingAlert == null)
                return NotFound("Alert not found");

            if (alertUpdateDto.StreetlightId != null)
            {
                existingAlert.StreetlightId = alertUpdateDto.StreetlightId;
                var streetLight = await _streetlightRepository.GetByIdAsync((int)alertUpdateDto.StreetlightId);
                existingAlert.Streetlight = streetLight;
            }

            if (alertUpdateDto.SensorId != null)
            {
                existingAlert.SensorId = alertUpdateDto.SensorId;
                var sensor = await _sensorRepository.GetByIdAsync((int)alertUpdateDto.SensorId);
                existingAlert.Sensor = sensor;
            }

            if (alertUpdateDto.AlertType != null)
            {
                existingAlert.AlertType = alertUpdateDto.AlertType;
            }

            if (alertUpdateDto.Resolved != null)
            {
                existingAlert.Resolved = (bool)alertUpdateDto.Resolved;
            }

            var updatedAlert = await _alertRepository.UpdateAsync(existingAlert);

            var updatedAlertDto = new AlertDto(
                updatedAlert.Id,
                updatedAlert.StreetlightId,
                updatedAlert.SensorId,
                updatedAlert.AlertType,
                updatedAlert.AlertDateTime,
                updatedAlert.Resolved
            );

            return Ok(updatedAlertDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            var success = await _alertRepository.DeleteAsync(id);
            if (!success)
                return NotFound("Alert not found");

            return NoContent();
        }
    }
}
