using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Dtos;
using SmartLightSense.Models;
using SmartLightSense.Repositories;

namespace SmartLightSense.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyUsageController : ControllerBase
    {
        private readonly IEnergyUsageRepository _energyUsageRepository;
        private readonly IStreetlightRepository _streetLightRepository;

        public EnergyUsageController(IEnergyUsageRepository energyUsageRepository, IStreetlightRepository streetLightRepository)
        {
            _energyUsageRepository = energyUsageRepository;
            _streetLightRepository = streetLightRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnergyUsageCreateDto energyUsageCreateDto)
        {
            var streetLight = await _streetLightRepository.GetByIdAsync(energyUsageCreateDto.StreetlightId);
            var energyUsage = new EnergyUsage
            {
                StreetlightId = energyUsageCreateDto.StreetlightId,
                Date = DateTime.Now,
                EnergyConsumed = energyUsageCreateDto.EnergyConsumed,
                Streetlight = streetLight
            };

            var createdEnergyUsage = await _energyUsageRepository.CreateAsync(energyUsage);

            var result = new EnergyUsageDto(
                createdEnergyUsage.Id,
                createdEnergyUsage.StreetlightId,
                createdEnergyUsage.Date,
                createdEnergyUsage.EnergyConsumed
            );

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EnergyUsageUpdateDto energyUsageUpdateDto)
        {
            var energyUsage = await _energyUsageRepository.GetByIdAsync(id);
            if (energyUsage == null)
                return NotFound();

            if (energyUsageUpdateDto.StreetlightId != null)
            {
                energyUsage.StreetlightId = (int)energyUsageUpdateDto.StreetlightId;
                var streetLight = await _streetLightRepository.GetByIdAsync((int)energyUsageUpdateDto.StreetlightId);
                energyUsage.Streetlight = streetLight;
            }

            if (energyUsageUpdateDto.EnergyConsumed != null)
            {
                energyUsage.EnergyConsumed = (double)energyUsageUpdateDto.EnergyConsumed;
            }

            var updatedEnergyUsage = await _energyUsageRepository.UpdateAsync(energyUsage);
            if (updatedEnergyUsage == null)
                return NotFound();

            var updatedEnergyUsageDto = new EnergyUsageDto(
                updatedEnergyUsage.Id,
                updatedEnergyUsage.StreetlightId,
                updatedEnergyUsage.Date,
                updatedEnergyUsage.EnergyConsumed
            );

            return Ok(updatedEnergyUsageDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _energyUsageRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var energyUsage = await _energyUsageRepository.GetByIdAsync(id);
            if (energyUsage == null)
                return NotFound();

            var energyUsageDto = new EnergyUsageDto(
                energyUsage.Id,
                energyUsage.StreetlightId,
                energyUsage.Date,
                energyUsage.EnergyConsumed
            );

            return Ok(energyUsageDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var energyUsages = await _energyUsageRepository.GetAllAsync();

            var energyUsageDtos = energyUsages.Select(usage => new EnergyUsageDto(
                usage.Id,
                usage.StreetlightId,
                usage.Date,
                usage.EnergyConsumed
            )).ToList();

            return Ok(energyUsageDtos);
        }
    }
}
