using Microsoft.AspNetCore.Mvc;
using WWW.Service.Helpers;

namespace WWW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Swaggerontroller : ControllerBase
    {
        private readonly RestApiRequest _restApiRequest;

        public Swaggerontroller(RestApiRequest restApiRequest) {
            _restApiRequest=restApiRequest;
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("EventApiGetAll")]
        public async Task<string> EventApiGetAll()
        {
            _restApiRequest.ApiSelector("Events:ticketmaster");
            
            return (await _restApiRequest.GetDataAsync())._embedded.events.name;
        }
    }
}
