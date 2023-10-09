using AutoMapper;
using CityWeather.DTOs;
using CityWeather.Interfaces;
using CityWeather.Model;
using Exceptions.Exeptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CityWeather.Services
{
    public class CityWeatherServices : ICityWeatherServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ICityWeatherServices> _logger;

        public CityWeatherServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ICityWeatherServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AvailableCitiesDTO>> GetAvailableCities()
        {
            _logger.LogInformation("Enter available cities service.");
            var citiesQuery = await _unitOfWork._cityRepository.GetAllAsync();
            var cities = citiesQuery.ToList();

            var returnValue = _mapper.Map<List<AvailableCitiesDTO>>(cities);

            _logger.LogInformation("Successfully exit available cities service.");
            return returnValue;
        }

        public async Task<List<CityAverageTemperatureDTO>> GetCityAverageTemperatures(CityTemperatureRequestDTO request)
        {
            _logger.LogInformation("Enter city average Temperatures service.");
            request.Days = request.Days == 0 ? 5 : request.Days;

            var distinctCityIds = request.CityIds?.Distinct().ToList();

            var citiesQuery = await _unitOfWork._cityRepository.GetAllAsync();

            if (!distinctCityIds.IsNullOrEmpty())
            {
                distinctCityIds.ForEach(cId =>
                {
                    City city = citiesQuery.Where(c => c.Id == cId).FirstOrDefault();

                    if (city == null)
                    {
                        _logger.LogWarning(string.Format("Exit city average Temperatures service with error because there is no city with id:{0}.", cId));
                        throw new NotFoundException(string.Format("There is no city with id:{0}.", cId));
                    }
                });
            }

            var citiesWithTemperatures = citiesQuery
                .Include(c => c.Temperatures)
                .Where(c => distinctCityIds.IsNullOrEmpty() || distinctCityIds.Contains(c.Id))
                .Select(c => new City
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    Temperatures = c.Temperatures
                        .Where(t => t.Date - DateTime.UtcNow <= new TimeSpan(request.Days, 0, 0, 0))
                        .ToList()
                })
                .ToList();

            var cityAverageTemperatures = _mapper.Map<List<CityAverageTemperatureDTO>>(citiesWithTemperatures);

            cityAverageTemperatures.ForEach(c =>
            {
                c.AverageTemperature = Math.Round(citiesWithTemperatures.Where(cwt => cwt.Id == c.Id).First()
                .Temperatures.Average(t => t.Value), 2);
            });

            var returnValue = cityAverageTemperatures.OrderBy(c => c.AverageTemperature).ToList();

            _logger.LogInformation("Successfully exit city average Temperatures service.");
            return returnValue;
        }
    }
}
