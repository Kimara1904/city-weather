namespace CityWeather.Model
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public virtual List<Temperature> Temperatures { get; set; } = null!;
    }
}
