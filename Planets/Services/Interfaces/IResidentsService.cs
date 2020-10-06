using Planets.ServiceModels;
using System.Threading.Tasks;

namespace Planets.Services.Interfaces
{
    public interface IResidentsService
    {
        Task<Resident> GetByPlanetIdAndSequenceOrDefaultAsync(int planetId, int sequenceNumber);
    }
}
