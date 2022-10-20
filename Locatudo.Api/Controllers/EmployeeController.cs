using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/employees")]
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
        public IActionResult List([FromServices] IEmployeeRepository employeeRepository)
        {
            var employees = employeeRepository.List<EmployeeDto>();
            return new OkObjectResult(employees);
        }
    }
}
