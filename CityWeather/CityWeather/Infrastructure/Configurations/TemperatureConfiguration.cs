using CityWeather.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityWeather.Infrastructure.Configurations
{
    public class TemperatureConfiguration : IEntityTypeConfiguration<Temperature>
    {
        public void Configure(EntityTypeBuilder<Temperature> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Value).IsRequired();
            builder.Property(t => t.Date).IsRequired();
            builder.HasOne(t => t.City).WithMany(c => c.Temperatures).HasForeignKey(t => t.CityId);
        }
    }
}
