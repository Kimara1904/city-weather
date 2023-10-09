namespace CityWeather.DTOs
{
    public class CityAverageTemperatureDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public double AverageTemperature { get; set; }
    }
}
