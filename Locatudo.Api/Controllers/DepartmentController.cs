using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/departments")]
    public class DepartmentController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IDepartmentRepository departmentRepository)
        {
            var departments = departmentRepository.List<DepartmentDto>();
            return new OkObjectResult(departments);
        }
    }
}
