using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planets.Controllers
{

    //




    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://swapi.dev/");


                var planetsResponse = await client.GetAsync("/api/planets/");

                var content = await planetsResponse.Content.ReadAsStringAsync();

                return new OkObjectResult(content);
            }
        }

        //first person that lives on a planet, 

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://swapi.dev/");


                var planetsResponse = await client.GetAsync($"api/planets/{id}/");

                var content = await planetsResponse.Content.ReadAsStringAsync();

                return new OkObjectResult(content);
            }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Planet
        {
            public string name { get; set; }
            public string rotation_period { get; set; }
            public string orbital_period { get; set; }
            public string diameter { get; set; }
            public string climate { get; set; }
            public string gravity { get; set; }
            public string terrain { get; set; }
            public string surface_water { get; set; }
            public string population { get; set; }
            public List<string> residents { get; set; }
            public List<string> films { get; set; }
            public DateTime created { get; set; }
            public DateTime edited { get; set; }
            public string url { get; set; }
        }

        //grab by item in the list
        [HttpGet("{id}/People/{sequence}")]
        public async Task<ActionResult> Get(int id, int sequence)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://swapi.dev/");


                var planetsResponse = await client.GetAsync($"api/planets/{id}/");

                var content = await planetsResponse.Content.ReadAsStringAsync();

                var planet = JsonConvert.DeserializeObject<Planet>(content);


                var residentPath = planet.residents[sequence - 1];

                var residentResponse = await client.GetAsync(residentPath);

                var residentContent = await residentResponse.Content.ReadAsStringAsync();

                //residents
                return new OkObjectResult(residentContent);
            }
        }



        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
