namespace CityWeather.Model.ApiModels
{
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;
    }
}
