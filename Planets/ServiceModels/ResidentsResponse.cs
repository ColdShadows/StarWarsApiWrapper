using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Planets.ServiceModels
{
    public class ResidentsResponse
    {
        public ResidentsResponse()
        {
            Residents = new List<Resident>();
        }

        public IEnumerable<Resident> Residents { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
