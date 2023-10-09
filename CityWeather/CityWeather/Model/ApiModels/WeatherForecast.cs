namespace CityWeather.Model.ApiModels
{
    public class WeatherForecast
    {
        public string Cod { get; set; } = null!;
        public int Message { get; set; }
        public int Cnt { get; set; }
        public List<WeatherData> List { get; set; } = null!;
        public City City { get; set; } = null!;
    }
}
