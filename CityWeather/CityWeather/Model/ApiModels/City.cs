namespace CityWeather.Model.ApiModels
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Coord Coord { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int Population { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
