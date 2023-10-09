using Newtonsoft.Json;

namespace CityWeather.Model.ApiModels
{
    public class WeatherData
    {
        public string Dt { get; set; } = null!;
        public MainData Main { get; set; } = null!;
        public List<Weather> Weather { get; set; } = null!;
        public Clouds Clouds { get; set; } = null!;
        public Wind Wind { get; set; } = null!;
        public int Visibility { get; set; }
        public Sys Sys { get; set; } = null!;
        [JsonProperty("dt_txt")]
        public DateTime DtTxt { get; set; }
    }
}
