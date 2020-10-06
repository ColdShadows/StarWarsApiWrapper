using AutoMapper;
using System.Collections.Generic;

namespace Planets.ServiceModels
{
    public class StarWarsMappingProfile: Profile
    {
        public StarWarsMappingProfile()
        {
            CreateMap<StarWarsPlanet, Planet>();
            CreateMap<StarWarsResident, Resident>();
            MapStarWarsPlanetsResponseToPlanets();
        }

        private void MapStarWarsPlanetsResponseToPlanets()
        {
            CreateMap<StarWarsPlanetsResponse, IEnumerable<Planet>>()
                .ConvertUsing(new StarWarsPlanetsResponseConverter());
        }        
    }

    public class StarWarsPlanetsResponseConverter : ITypeConverter<StarWarsPlanetsResponse, IEnumerable<Planet>>
    {
        public IEnumerable<Planet> Convert(StarWarsPlanetsResponse source, IEnumerable<Planet> destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            var planets = context.Mapper.Map<IEnumerable<Planet>>(source.Results);

            return planets;
        }
    }
}
