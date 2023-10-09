using AutoMapper;
using CityWeather.DTOs;
using CityWeather.Model;

namespace CityWeather.Mapper
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, AvailableCitiesDTO>();
            CreateMap<City, CityAverageTemperatureDTO>();
        }
    }
}
