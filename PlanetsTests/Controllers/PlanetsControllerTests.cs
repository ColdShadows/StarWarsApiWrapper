using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Planets.ServiceModels;
using Planets.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Planets.Controllers
{
    public class PlanetsControllerTests
    {
        [Fact]
        public async Task Get_PlanetIsNotNull_ReturnOkObjectResultWithPlanet()
        {
            //Arrange
            var planetId = 67;
            var planetService = Substitute.For<IPlanetsService>();
            var expected = new Planet();
            planetService.GetPlanetOrDefaultAsync(Arg.Is(planetId)).Returns(Task.FromResult(expected));

            //Subject Under Test
            var sut = new PlanetsController(planetService);

            //Act
            var result = await sut.Get(planetId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public async Task Get_PlanetIsNull_ReturnNotFoundResult()
        {
            //Arrange
            var planetId = 67;
            var planetService = Substitute.For<IPlanetsService>();
            var expected = (Planet)null;
            planetService.GetPlanetOrDefaultAsync(Arg.Is(planetId)).Returns(Task.FromResult(expected));

            var sut = new PlanetsController(planetService);

            //Act
            var result = await sut.Get(planetId);

            //Assert            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
