using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/departments")]
    [Produces("application/json")]
    public class DepartmentController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DepartmentDto>))]
        public IActionResult List([FromServices] IDepartmentRepository departmentRepository)
        {
            var departments = departmentRepository.List<DepartmentDto>();
            return new OkObjectResult(departments);
        }
    }
}
