using Microsoft.AspNetCore.Mvc;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("")]
    [Produces("application/json")]
    public class HomeController : Controller
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Index()
        {
            return new OkObjectResult("Bem vindo à API de Locatudo!");
        }
    }
}
