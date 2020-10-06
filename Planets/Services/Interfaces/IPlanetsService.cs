using Planets.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planets.Services.Interfaces
{
    public interface IPlanetsService
    {
        Task<PlanetsResponse> GetPlanetsAsync();
        Task<Planet> GetPlanetOrDefaultAsync(int id);
    }
}
