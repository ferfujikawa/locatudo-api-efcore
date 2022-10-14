using AutoMapper;
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
        public IActionResult List(
            [FromServices] IDepartmentRepository departmentRepository,
            [FromServices] IMapper mapper)
        {
            var departments = departmentRepository.List();
            return new OkObjectResult(mapper.Map<IEnumerable<DepartmentDto>>(departments));
        }
    }
}
