using AutoMapper;
using Newtonsoft.Json;
using Planets.ServiceModels;
using Planets.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Planets.Services
{
    public class StarWarsPlanetsService : IPlanetsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public StarWarsPlanetsService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<PlanetsResponse> GetPlanetsAsync()
        {
            var planetsResponse = new PlanetsResponse();

            var starWarsPlanetsResponse = await _httpClient.GetAsync("/api/planets/");

            if (!starWarsPlanetsResponse.IsSuccessStatusCode)
            {
                throw new ExternalException("We're having problems with our StarWars Data Provider");
            }

            var content = await starWarsPlanetsResponse.Content.ReadAsStringAsync();
            var starWarsPlanets = JsonConvert.DeserializeObject<StarWarsPlanetsResponse>(content);

            planetsResponse.Planets = _mapper.Map<IEnumerable<Planet>>(starWarsPlanets);

            return planetsResponse;
        }

        //Do or Do Not. There is no retry.
        public async Task<Planet> GetPlanetOrDefaultAsync(int id)
        {
            var starWarsPlanetsResponse = await _httpClient.GetAsync($"api/planets/{id}/");

            if (starWarsPlanetsResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (!starWarsPlanetsResponse.IsSuccessStatusCode)
            {
                throw new ExternalException("We're having problems with our StarWars Data Provider");
            }

            var content = await starWarsPlanetsResponse.Content.ReadAsStringAsync();
            var starWarsPlanet = JsonConvert.DeserializeObject<StarWarsPlanet>(content);

            return _mapper.Map<Planet>(starWarsPlanet);
        }
    }
}
