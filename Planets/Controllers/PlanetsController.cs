using Microsoft.AspNetCore.Mvc;
using Planets.Services.Interfaces;
using System.Threading.Tasks;

namespace Planets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetsService _planetService;
        public PlanetsController(IPlanetsService planetService)
        {
            _planetService = planetService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var planets = await _planetService.GetPlanetsAsync();

            return new OkObjectResult(planets.Planets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var planet = await _planetService.GetPlanetOrDefaultAsync(id);

            if(planet == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(planet);
        }           
    }
}
