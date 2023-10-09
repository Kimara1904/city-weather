using CityWeather.Model;
using Microsoft.EntityFrameworkCore;

namespace CityWeather.Infrastructure
{
    public class CityWeatherDBContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; }

        public CityWeatherDBContext(DbContextOptions<CityWeatherDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CityWeatherDBContext).Assembly);
        }
    }
}
