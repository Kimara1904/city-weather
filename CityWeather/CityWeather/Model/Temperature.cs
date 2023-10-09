namespace CityWeather.Model
{
    public class Temperature
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
