using Locatudo.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class DepartmentController : Controller
    {
        [HttpGet]
        [Route("departments")]
        public IActionResult List([FromServices] IDepartmentRepository departmentRepository)
        {
            var departments = departmentRepository.List();
            return new OkObjectResult(departments);
        }
    }
}
