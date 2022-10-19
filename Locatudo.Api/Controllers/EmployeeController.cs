using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/employees")]
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IEmployeeRepository employeeRepository)
        {
            var employees = employeeRepository.List<EmployeeDto>();
            return new OkObjectResult(employees);
        }
    }
}
