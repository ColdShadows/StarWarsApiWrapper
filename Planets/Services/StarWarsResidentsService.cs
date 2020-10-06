using AutoMapper;
using Newtonsoft.Json;
using Planets.ServiceModels;
using Planets.Services.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace Planets.Services
{
    public class StarWarsResidentsService : IResidentsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IPlanetsService _planetsService;

        public StarWarsResidentsService(HttpClient httpClient, IMapper mapper, IPlanetsService planetsService)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _planetsService = planetsService;
        }


        public async Task<Resident> GetByPlanetIdAndSequenceOrDefaultAsync(int planetId, int sequenceNumber)
        {
            if (sequenceNumber < 1 || planetId < 1)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var planet = await _planetsService.GetPlanetOrDefaultAsync(planetId);

            if(planet == null)
            {
                return null;
            }

            if(planet.Residents.Count() < sequenceNumber || sequenceNumber < 1)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var starWarsResidentPath = planet.Residents[sequenceNumber - 1];

            var starWarsResidentResponse = await _httpClient.GetAsync(starWarsResidentPath);

            if (starWarsResidentResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (!starWarsResidentResponse.IsSuccessStatusCode)
            {
                throw new ExternalException("We're having problems with our StarWars Data Provider");
            }

            var starWarsResidentContent = await starWarsResidentResponse.Content.ReadAsStringAsync();

            var starWarsResident = JsonConvert.DeserializeObject<StarWarsResident>(starWarsResidentContent);

            return _mapper.Map<Resident>(starWarsResident);
        }
    }
}
