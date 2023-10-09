using Newtonsoft.Json;

namespace CityWeather.Model.ApiModels
{
    public class MainData
    {
        public double Temp { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public int GrndLevel { get; set; }
        public int Humidity { get; set; }
        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }
}
