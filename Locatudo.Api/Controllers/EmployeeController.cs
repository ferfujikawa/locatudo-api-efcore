using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/employees")]
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Create([FromServices] IEmployeeRepository employeeRepository)
        {
            var employees = employeeRepository.List();
            return new OkObjectResult(employees);
        }
    }
}
