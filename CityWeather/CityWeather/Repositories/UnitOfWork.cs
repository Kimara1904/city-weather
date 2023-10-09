using CityWeather.Interfaces;
using CityWeather.Model;
using Microsoft.EntityFrameworkCore;

namespace CityWeather.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public IGenericRepository<City> _cityRepository { get; } = null!;

        public IGenericRepository<Temperature> _temperatureRepository { get; } = null!;

        public UnitOfWork(DbContext context, IGenericRepository<City> cityRepository,
            IGenericRepository<Temperature> temperatureRepository)
        {
            _context = context;
            _cityRepository = cityRepository;
            _temperatureRepository = temperatureRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
