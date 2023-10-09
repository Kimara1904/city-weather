using CityWeather.DTOs;
using System.Diagnostics.CodeAnalysis;

namespace CityWeatherTest.Comparers
{
    public class CityDTOEqualityComparer : IEqualityComparer<AvailableCitiesDTO>
    {
        public bool Equals(AvailableCitiesDTO x, AvailableCitiesDTO y)
        {
            return x.Id == y.Id && x.CityName == y.CityName;
        }

        public int GetHashCode([DisallowNull] AvailableCitiesDTO obj)
        {
            return obj.Id.GetHashCode() ^ obj.CityName.GetHashCode();
        }
    }
}
