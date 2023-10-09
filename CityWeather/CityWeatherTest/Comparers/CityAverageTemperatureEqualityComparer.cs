using CityWeather.DTOs;

namespace CityWeatherTest.Comparers
{
    public class CityAverageTemperatureEqualityComparer : IEqualityComparer<CityAverageTemperatureDTO>
    {
        public bool Equals(CityAverageTemperatureDTO x, CityAverageTemperatureDTO y)
        {
            return x.Id == y.Id && x.CityName == y.CityName && x.AverageTemperature == y.AverageTemperature;
        }

        public int GetHashCode(CityAverageTemperatureDTO obj)
        {
            return obj.Id.GetHashCode() ^ obj.CityName.GetHashCode() ^ obj.AverageTemperature.GetHashCode();
        }
    }
}
