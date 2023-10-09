using AutoMapper;
using CityWeather.Controllers;
using CityWeather.DTOs;
using CityWeather.Interfaces;
using CityWeather.Mapper;
using CityWeather.Model;
using CityWeather.Services;
using CityWeather.Validators;
using CityWeatherTest.Comparers;
using Exceptions.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CityWeatherTest
{
    public class AverageTemperatureTest
    {
        [Fact]
        public async Task GetCityAverageTemperature_EmptyCityList_3Days_ReturnsAverageForAllCities()
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

            var cityList = new List<City>
            {
                new City { Id = 1, CityName = "City 1",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 1, CityId = 1, Value = 13.75, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 2, CityId = 1, Value = 14.25, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 3, CityId = 1, Value = 14.75, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0) },
                        new Temperature { Id = 4, CityId = 1, Value = 15.00, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 5, CityId = 1, Value = 15.50, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                },
                new City { Id = 2, CityName = "City 2",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 6, CityId = 2, Value = 12.50, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 7, CityId = 2, Value = 12.75, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 8, CityId = 2, Value = 13.00, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0)},
                        new Temperature { Id = 9, CityId = 2, Value = 13.25, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 10, CityId = 2, Value = 13.50, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                },
                new City { Id = 3, CityName = "City 3",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 11, CityId = 3, Value = 16.00, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 12, CityId = 3, Value = 16.25, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 13, CityId = 3, Value = 16.50, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0) },
                        new Temperature { Id = 14, CityId = 3, Value = 16.75, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 15, CityId = 3, Value = 17.00, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                }
            };

            var expectedResult = new List<CityAverageTemperatureDTO>
            {
                new CityAverageTemperatureDTO { Id = 2, CityName = "City 2", AverageTemperature = 12.75 },
                new CityAverageTemperatureDTO { Id = 1, CityName = "City 1", AverageTemperature = 14.25 },
                new CityAverageTemperatureDTO { Id = 3, CityName = "City 3", AverageTemperature = 16.25 }
            };

            var request = new CityTemperatureRequestDTO() { Days = 3, CityIds = new List<int>() };

            unitOfWorkMock.Setup(u => u._cityRepository.GetAllAsync())
                .Returns(Task.FromResult(cityList.AsQueryable()));

            // Act
            var result = await controller.GetAverageTemperature(request);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<CityAverageTemperatureDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            //Check status code
            Assert.Equal(200, okResult.StatusCode);

            var list = Assert.IsType<List<CityAverageTemperatureDTO>>(okResult.Value);
            Assert.NotEmpty(list);
            Assert.Equal(3, list.Count);
            Assert.True(list.SequenceEqual(expectedResult, new CityAverageTemperatureEqualityComparer()));
        }

        [Fact]
        public async Task GetCityAverageTemperature_2CityIds_0Days_ReturnsAverageFor5Days()
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

            var cityList = new List<City>
            {
                new City { Id = 1, CityName = "City 1",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 1, CityId = 1, Value = 13.75, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 2, CityId = 1, Value = 14.25, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 3, CityId = 1, Value = 14.75, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0) },
                        new Temperature { Id = 4, CityId = 1, Value = 15.00, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 5, CityId = 1, Value = 15.50, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                },
                new City { Id = 2, CityName = "City 2",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 6, CityId = 2, Value = 12.50, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 7, CityId = 2, Value = 12.75, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 8, CityId = 2, Value = 13.00, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0)},
                        new Temperature { Id = 9, CityId = 2, Value = 13.25, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 10, CityId = 2, Value = 13.50, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                },
                new City { Id = 3, CityName = "City 3",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 11, CityId = 3, Value = 16.00, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 12, CityId = 3, Value = 16.25, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 13, CityId = 3, Value = 16.50, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0) },
                        new Temperature { Id = 14, CityId = 3, Value = 16.75, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 15, CityId = 3, Value = 17.00, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                }
            };

            var expectedResult = new List<CityAverageTemperatureDTO>
            {
                new CityAverageTemperatureDTO { Id = 2, CityName = "City 2", AverageTemperature = 13.00 },
                new CityAverageTemperatureDTO { Id = 3, CityName = "City 3", AverageTemperature = 16.50 }
            };

            unitOfWorkMock.Setup(u => u._cityRepository.GetAllAsync())
                .Returns(Task.FromResult(cityList.AsQueryable()));

            // Act
            var result = await controller.GetAverageTemperature(new CityTemperatureRequestDTO() { Days = 0, CityIds = new List<int>() { 2, 3 } });

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<CityAverageTemperatureDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            //Check status code
            Assert.Equal(200, okResult.StatusCode);

            Assert.NotNull(result);
            var list = Assert.IsType<List<CityAverageTemperatureDTO>>(okResult.Value);
            Assert.NotEmpty(list);
            Assert.Equal(2, list.Count);
            Assert.True(list.SequenceEqual(expectedResult, new CityAverageTemperatureEqualityComparer()));
        }

        [Fact]
        public async Task GetCityAverageTemperature_IdOfNotAvailable()
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

            var cityList = new List<City>
            {
                new City { Id = 1, CityName = "City 1" },
                new City { Id = 2, CityName = "City 2" },
                new City { Id = 3, CityName = "City 3" }
            };

            unitOfWorkMock.Setup(u => u._cityRepository.GetAllAsync())
                 .Returns(Task.FromResult(cityList.AsQueryable()));

            // Act
            async Task Action() => await controller.GetAverageTemperature(new CityTemperatureRequestDTO() { Days = 0, CityIds = new List<int>() { 2147714, 1 } });

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Fact]
        public async Task GetCityAverageTemperature_2SameIds()
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

            var cityList = new List<City>
            {
                new City { Id = 1, CityName = "City 1",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 1, CityId = 1, Value = 13.75, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 2, CityId = 1, Value = 14.25, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 3, CityId = 1, Value = 14.75, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0) },
                        new Temperature { Id = 4, CityId = 1, Value = 15.00, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 5, CityId = 1, Value = 15.50, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                },
                new City { Id = 2, CityName = "City 2",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 6, CityId = 2, Value = 12.50, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 7, CityId = 2, Value = 12.75, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 8, CityId = 2, Value = 13.00, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0)},
                        new Temperature { Id = 9, CityId = 2, Value = 13.25, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 10, CityId = 2, Value = 13.50, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                },
                new City { Id = 3, CityName = "City 3",
                    Temperatures = new List<Temperature>
                    {
                        new Temperature { Id = 11, CityId = 3, Value = 16.00, Date = DateTime.UtcNow + new TimeSpan(1, 0, 0, 0) },
                        new Temperature { Id = 12, CityId = 3, Value = 16.25, Date = DateTime.UtcNow + new TimeSpan(2, 0, 0, 0) },
                        new Temperature { Id = 13, CityId = 3, Value = 16.50, Date = DateTime.UtcNow + new TimeSpan(3, 0, 0, 0) },
                        new Temperature { Id = 14, CityId = 3, Value = 16.75, Date = DateTime.UtcNow + new TimeSpan(4, 0, 0, 0) },
                        new Temperature { Id = 15, CityId = 3, Value = 17.00, Date = DateTime.UtcNow + new TimeSpan(5, 0, 0, 0) }
                    }
                }
            };

            var expectedResult = new List<CityAverageTemperatureDTO>
            {
                new CityAverageTemperatureDTO { Id = 1, CityName = "City 1", AverageTemperature = 14.00 }
            };

            unitOfWorkMock.Setup(u => u._cityRepository.GetAllAsync())
                .Returns(Task.FromResult(cityList.AsQueryable()));

            // Act
            var result = await controller.GetAverageTemperature(new CityTemperatureRequestDTO() { Days = 2, CityIds = new List<int>() { 1, 1 } });

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<CityAverageTemperatureDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            //Check status code
            Assert.Equal(200, okResult.StatusCode);

            Assert.NotNull(result);
            var list = Assert.IsType<List<CityAverageTemperatureDTO>>(okResult.Value);
            Assert.NotEmpty(list);
            Assert.Single(list);
            Assert.True(list.SequenceEqual(expectedResult, new CityAverageTemperatureEqualityComparer()));
        }

        [Fact]
        public async Task GetCityAverageTemperature_GreaterThan5Days()
        {
            var validator = new CityTemperatureRequestDTOValidator();
            var request = new CityTemperatureRequestDTO() { Days = 45, CityIds = new List<int>() { 1 } };

            // Act
            var validationResult = await validator.ValidateAsync(request);

            // Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task GetCityAverageTemperature_LessThan0Days()
        {
            // Arrange
            var validator = new CityTemperatureRequestDTOValidator();
            var request = new CityTemperatureRequestDTO() { Days = -1, CityIds = new List<int>() { 1 } };

            // Act
            var validationResult = await validator.ValidateAsync(request);

            // Assert
            Assert.False(validationResult.IsValid);
        }
    }
}
