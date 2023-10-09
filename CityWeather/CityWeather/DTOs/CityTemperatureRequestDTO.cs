namespace CityWeather.DTOs
{
    public class CityTemperatureRequestDTO
    {
        public List<int>? CityIds { get; set; }
        public int Days { get; set; }
    }
}
