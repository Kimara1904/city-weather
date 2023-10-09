using CityWeather.DTOs;
using FluentValidation;

namespace CityWeather.Validators
{
    public class CityTemperatureRequestDTOValidator : AbstractValidator<CityTemperatureRequestDTO>
    {
        public CityTemperatureRequestDTOValidator()
        {
            RuleFor(x => x.Days).InclusiveBetween(0, 5);
        }
    }
}
