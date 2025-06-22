using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;

namespace SmartLightSense.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceLogsController : ControllerBase
    {
        private readonly IMaintenanceLogRepository _maintenanceLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStreetlightRepository _streetlightRepository;

        public MaintenanceLogsController(IMaintenanceLogRepository maintenanceLogRepository, IUserRepository userRepository, IStreetlightRepository streetlightRepository)
        {
            _maintenanceLogRepository = maintenanceLogRepository;
            _userRepository = userRepository;
            _streetlightRepository = streetlightRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaintenanceLog([FromBody] MaintenanceLogCreateDto createDto)
        {
            var technician = await _userRepository.GetByIdAsync(createDto.TechnicianId);
            var maintenanceLog = new MaintenanceLog
            {
                StreetlightId = createDto.StreetlightId,
                TechnicianId = createDto.TechnicianId,
                AlertId = createDto.AlertId,
                Date = DateTime.Now,
                IssueReported = createDto.IssueReported,
                ActionTaken = createDto.ActionTaken,
                Status = createDto.Status
            };

            var createdLog = await _maintenanceLogRepository.CreateAsync(maintenanceLog);

            var result = new MaintenanceLogDto(
                createdLog.Id,
                createdLog.StreetlightId,
                createdLog.AlertId,
                createdLog.TechnicianId,
                createdLog.Date,
                createdLog.IssueReported,
                createdLog.ActionTaken,
                createdLog.Status
            );

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenanceLog(int id, [FromBody] MaintenanceLogUpdateDto updateDto)
        {
            var maintenanceLog = await _maintenanceLogRepository.GetByIdAsync(id);
            if (maintenanceLog == null)
                return NotFound();

            if (updateDto.StreetlightId != null)
            {
                maintenanceLog.StreetlightId = updateDto.StreetlightId;
            }

            if (updateDto.TechnicianId != null)
            {
                maintenanceLog.TechnicianId = (int)updateDto.TechnicianId;
            }

            if (updateDto.AlertId != null)
            {
                maintenanceLog.AlertId = updateDto.AlertId;
            }

            if (updateDto.IssueReported != null)
            {
                maintenanceLog.IssueReported = updateDto.IssueReported;
            }

            if (updateDto.ActionTaken != null)
            {
                maintenanceLog.ActionTaken = updateDto.ActionTaken;
            }

            if (updateDto.Status != null)
            {
                maintenanceLog.Status = updateDto.Status;
            }

            var result = await _maintenanceLogRepository.UpdateAsync(maintenanceLog);

            var updatedLogDto = new MaintenanceLogDto(
                maintenanceLog.Id,
                maintenanceLog.StreetlightId,
                maintenanceLog.AlertId,
                maintenanceLog.TechnicianId,
                maintenanceLog.Date,
                maintenanceLog.IssueReported,
                maintenanceLog.ActionTaken,
                maintenanceLog.Status
            );

            return Ok(updatedLogDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceLog(int id)
        {
            var result = await _maintenanceLogRepository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaintenanceLog(int id)
        {
            var maintenanceLog = await _maintenanceLogRepository.GetByIdAsync(id);
            if (maintenanceLog == null)
                return NotFound();

            var maintenanceLogDto = new MaintenanceLogDto(
                maintenanceLog.Id,
                maintenanceLog.StreetlightId,
                maintenanceLog.AlertId,
                maintenanceLog.TechnicianId,
                maintenanceLog.Date,
                maintenanceLog.IssueReported,
                maintenanceLog.ActionTaken,
                maintenanceLog.Status
            );

            return Ok(maintenanceLogDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaintenanceLogs()
        {
            var maintenanceLogs = await _maintenanceLogRepository.GetAllAsync();

            var maintenanceLogsDto = maintenanceLogs.Select(log => new MaintenanceLogDto(
                log.Id,
                log.StreetlightId,
                log.AlertId,
                log.TechnicianId,
                log.Date,
                log.IssueReported,
                log.ActionTaken,
                log.Status
            )).ToList();

            return Ok(maintenanceLogsDto);
        }
    }
}
