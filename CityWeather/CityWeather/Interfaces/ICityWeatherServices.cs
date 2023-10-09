using CityWeather.DTOs;

namespace CityWeather.Interfaces
{
    public interface ICityWeatherServices
    {
        Task<List<AvailableCitiesDTO>> GetAvailableCities();
        Task<List<CityAverageTemperatureDTO>> GetCityAverageTemperatures(CityTemperatureRequestDTO request);
    }
}
