using CityWeather.Model;

namespace CityWeather.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<City> _cityRepository { get; }
        IGenericRepository<Temperature> _temperatureRepository { get; }

        Task Save();
    }
}
