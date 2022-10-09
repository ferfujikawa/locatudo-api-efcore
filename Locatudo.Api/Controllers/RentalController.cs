using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/rentals")]
    public class RentalController : Controller
    {
        [HttpGet]
        public IActionResult Create([FromServices] IRentalRepository rentalRepository)
        {
            var rentals = rentalRepository.List();
            return new OkObjectResult(rentals);
        }
    }
}
