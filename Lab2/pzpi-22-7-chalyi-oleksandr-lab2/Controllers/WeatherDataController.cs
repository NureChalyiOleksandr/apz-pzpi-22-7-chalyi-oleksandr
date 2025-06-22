using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;

namespace SmartLightSense.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherDataController : ControllerBase
    {
        private readonly IWeatherDataRepository _weatherDataRepository;

        public WeatherDataController(IWeatherDataRepository weatherDataRepository)
        {
            _weatherDataRepository = weatherDataRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeatherDataDto>>> GetAll()
        {
            var weatherDataList = await _weatherDataRepository.GetAllAsync();

            return Ok(weatherDataList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherDataDto>> GetById(int id)
        {
            var weatherData = await _weatherDataRepository.GetByIdAsync(id);
            if (weatherData == null)
            {
                return NotFound();
            }

            return Ok(weatherData);
        }

        [HttpPost]
        public async Task<ActionResult<WeatherDataDto>> Create([FromBody] WeatherDataCreateDto weatherDataCreateDto)
        {
            var weatherData = new WeatherData
            {
                Date = DateTime.Now,
                Temperature = weatherDataCreateDto.Temperature,
                Visibility = weatherDataCreateDto.Visibility,
                Precipitation = weatherDataCreateDto.Precipitation
            };

            var createdWeatherData = await _weatherDataRepository.CreateAsync(weatherData);

            return Ok(createdWeatherData);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherDataDto>> Update(int id, [FromBody] WeatherDataUpdateDto weatherDataUpdateDto)
        {
            var weatherData = await _weatherDataRepository.GetByIdAsync(id);
            if (weatherData == null)
            {
                return NotFound();
            }

            if (weatherDataUpdateDto.Temperature != null)
            {
                weatherData.Temperature = (double)weatherDataUpdateDto.Temperature;
            }
            
            if (weatherDataUpdateDto.Visibility != null)
            {
                weatherData.Visibility = (double)weatherDataUpdateDto.Visibility;
            }
            
            if (weatherDataUpdateDto.Precipitation != null)
            {
                weatherData.Precipitation = (double)weatherDataUpdateDto.Precipitation;
            }
            
            var updatedWeatherData = await _weatherDataRepository.UpdateAsync(weatherData);

            return Ok(updatedWeatherData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _weatherDataRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
