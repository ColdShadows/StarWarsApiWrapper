using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planets.ServiceModels
{   
    public class StarWarsPlanetsResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        public List<StarWarsPlanet> Results { get; set; }
    }
}
