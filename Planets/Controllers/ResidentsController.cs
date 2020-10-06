using Microsoft.AspNetCore.Mvc;
using Planets.Services.Interfaces;
using System.Threading.Tasks;

namespace Planets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentsService _residentService;
        public ResidentsController(IResidentsService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet]
        [Route("/api/planets/{id}/residents/{sequence}")]
        public async Task<ActionResult> Get(int id, int sequence)
        {
            /* Not sure how you guys feel about the approach. I've seen it many ways based on preference.  
             * Status code in the return model from the service, try catch for specific types of exceptions, 
             * deriving status from null/magic values, etc. Just one way to get the job done. 
             */
            try
            {
                var resident = await _residentService.GetByPlanetIdAndSequenceOrDefaultAsync(id, sequence);

                if (resident == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(resident);
            }
            catch(System.Web.Http.HttpResponseException httpResponseException)
            {
                return new StatusCodeResult((int)httpResponseException.Response.StatusCode);
            }            
        }
    }
}
