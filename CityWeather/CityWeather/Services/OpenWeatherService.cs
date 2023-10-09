using CityWeather.Interfaces;
using CityWeather.Model;
using CityWeather.Model.ApiModels;
using Exceptions.Exeptions;
using Newtonsoft.Json;

namespace CityWeather.Services
{
    public class OpenWeatherService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OpenWeatherService> _logger;

        public OpenWeatherService(HttpClient httpClient, IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<OpenWeatherService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service cycles started...");
            stoppingToken.ThrowIfCancellationRequested();

            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var citiesQuery = await unitOfWork._cityRepository.GetAllAsync();
            var cities = citiesQuery.ToList();

            await unitOfWork._temperatureRepository.RemoveAllAsync();

            var tasks = cities.Select(c => TakeTemperatures(c, unitOfWork));
            await Task.WhenAll(tasks);

            await unitOfWork.Save();

            _logger.LogInformation("Background server cycles ended...");
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

            return Task.CompletedTask;
        }

        private async Task TakeTemperatures(Model.City city, IUnitOfWork unitOfWork)
        {
            _logger.LogInformation("Enter take temperature method.");
            var response = await _httpClient.GetAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?id={0}&appid={1}&units=metric", city.Id, _configuration["ApiKey"]));

            if (response == null)
            {
                _logger.LogError("Exit take temperature method because there is error with response.");
                throw new InternalServerErrorException("There is Internal Server Error, please try later.");
            }

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WeatherForecast>(json);

                if (data == null)
                {
                    _logger.LogError("Exit take temperature method because there is error with data.");
                    throw new InternalServerErrorException("There is Internal Server Error, please try later.");
                }

                data.List.ForEach(item =>
                {
                    var newTemperature = new Temperature
                    {
                        CityId = city.Id,
                        Value = item.Main.Temp,
                        Date = item.DtTxt
                    };
                    unitOfWork._temperatureRepository.Insert(newTemperature);
                });
            }
            else
            {
                _logger.LogError("Exit take temperature method because resonse is not success.");
                throw new InternalServerErrorException("There is Internal Server Error, please try later.");
            }
        }
    }
}
