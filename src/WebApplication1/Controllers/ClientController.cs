using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Models.Filters;
using Models.Mapper.Request;
using Models.Mapper.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet()]
        public IActionResult Get([FromQuery] ClientFilter clientFilter)
        {
            return Execute(() => _clientService.Search<ClientResponse>(clientFilter), 200, true);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClientPost clientPost)
        {
            return ExecuteCreate(() => _clientService.Create<ClientResponse>(clientPost));
        }
    }
}
