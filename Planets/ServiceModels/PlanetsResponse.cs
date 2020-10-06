using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Planets.ServiceModels
{
    public class PlanetsResponse
    {
        public PlanetsResponse()
        {
            Planets = new List<Planet>();
        }

        public IEnumerable<Planet> Planets { get; set; }
    }
}
