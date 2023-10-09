using AutoMapper;
using CityWeather.Controllers;
using CityWeather.DTOs;
using CityWeather.Interfaces;
using CityWeather.Mapper;
using CityWeather.Model;
using CityWeather.Services;
using CityWeatherTest.Comparers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CityWeatherTest
{
    public class AvailableCityTest
    {
        [Fact]
        public async Task GetAvailableCities_ReturnsOkResult()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CityProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            var loggerServerMock = new Mock<ILogger<CityWeatherServices>>();
            var useCaseServices = new CityWeatherServices(unitOfWorkMock.Object, mapper, loggerServerMock.Object);
            var loggerControllerMock = new Mock<ILogger<CityWeatherController>>();
            var controller = new CityWeatherController(useCaseServices, loggerControllerMock.Object);

            var expectedCities = new List<City>
            {
                new City { Id = 2147714, CityName = "Sydney" },
                new City { Id = 3194360, CityName = "Novi Sad" },
                new City { Id = 6173331, CityName = "Vancouver" }
            };

            unitOfWorkMock.Setup(u => u._cityRepository.GetAllAsync())
                .Returns(Task.FromResult(expectedCities.AsQueryable()));

            // Act
            var result = await controller.GetAvailableCities();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<AvailableCitiesDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            //Check status code
            Assert.Equal(200, okResult.StatusCode);

            //Check value
            var jsonResult = Assert.IsType<List<AvailableCitiesDTO>>(okResult.Value);

            Assert.True(mapper.Map<List<AvailableCitiesDTO>>(expectedCities).SequenceEqual(jsonResult, new CityDTOEqualityComparer()));
        }
    }
}
