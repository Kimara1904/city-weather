using CityWeather.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityWeather.Infrastructure.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasData(
                new City
                {
                    Id = 3194360,
                    CityName = "Novi Sad"
                },
                new City
                {
                    Id = 2147714,
                    CityName = "Sydney"
                },
                new City
                {
                    Id = 6173331,
                    CityName = "Vancouver"
                }
            );
        }
    }
}
