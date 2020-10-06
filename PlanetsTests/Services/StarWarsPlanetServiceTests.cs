using AutoMapper;

using NSubstitute;

using Planets.ServiceModels;
using PlanetsTests;

using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Xunit;

namespace Planets.Services
{
    public class StarWarsPlanetServiceTests
    {
        //I prefer the When_Given_Then naming style. Helps sort tests
        [Fact]
        public async Task GetPlanetsAsync_IsSuccessStatusCode_ReturnsPlanets()
        {
            //Arrange
            var expected = new List<Planet>();
            var mapper = Substitute.For<IMapper>();
            mapper.Map<IEnumerable<Planet>>(Arg.Any<StarWarsPlanetsResponse>()).Returns(expected);

            var client = MockHttpClientHelper.GetHttpClient("", HttpStatusCode.OK);

            var sut = new StarWarsPlanetsService(client, mapper);

            //Act
            var result = await sut.GetPlanetsAsync();

            //Assert
            Assert.Equal(expected, result.Planets);
        }       

        [Fact]
        public async Task GetPlanetsAsync_NotIsSuccessStatusCode_ThrowsExternalException()
        {
            //Arrange
            var planets = new List<Planet>();
            var expectedMessage = "We're having problems with our StarWars Data Provider";

            var mapper = Substitute.For<IMapper>();
            mapper.Map<IEnumerable<Planet>>(Arg.Any<StarWarsPlanetsResponse>()).Returns(planets);

            var client = MockHttpClientHelper.GetHttpClient("", HttpStatusCode.BadRequest);

            var sut = new StarWarsPlanetsService(client, mapper);

            //Act
            var exception = await Assert.ThrowsAsync<ExternalException>(async () => await sut.GetPlanetsAsync());

            //Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
