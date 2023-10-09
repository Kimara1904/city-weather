using CityWeather.DTOs;
using CityWeather.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CityWeather.Controllers
{
    [Route("api/city-weather")]
    [ApiController]
    public class CityWeatherController : ControllerBase
    {
        private readonly ICityWeatherServices _useCaseServices;
        private readonly ILogger<CityWeatherController> _logger;

        public CityWeatherController(ICityWeatherServices useCaseServices, ILogger<CityWeatherController> logger)
        {
            _useCaseServices = useCaseServices;
            _logger = logger;

        }

        /// <summary>
        /// Get all available cities.
        /// </summary>
        /// <returns>List of available cities, configurated in system.</returns>
        /// <response code="200">Returns the list of available cities</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("available-cities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AvailableCitiesDTO>>> GetAvailableCities()
        {
            _logger.LogInformation("Enter available cities controller.");
            var returnValue = await _useCaseServices.GetAvailableCities();
            _logger.LogInformation("Succsessfully exit available cities controller.");
            return Ok(returnValue);
        }

        /// <summary>
        /// Get average of Temperatures in Celsus for requested cities for requested number of days.
        /// </summary>
        /// <param name="request">Request with city ids and number of days. If city ids list is empty there will be returned 
        /// average Temperatures for all available cities. If number of days is 0, there will be returned average Temperatures
        /// for 5 days.</param>
        /// <returns>List of requested cities, sorted by average Temperature for requested days.</returns>
        /// <response code="200">List of requested cities with average Temperatures for requested days.</response>
        /// <response code="400">Bad Requset because number of days is out of range [0-5].</response>
        /// <response code="404">Requested city isn't available.</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("city-average-temperature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CityAverageTemperatureDTO>>> GetAverageTemperature([FromQuery] CityTemperatureRequestDTO request)
        {
            _logger.LogInformation("Enter average temperatures controller.");
            var returnValue = await _useCaseServices.GetCityAverageTemperatures(request);
            _logger.LogInformation("Successfully exit average temperatures controlller.");
            return Ok(returnValue);
        }
    }
}
