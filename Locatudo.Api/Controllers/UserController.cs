﻿using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IUserRepository userRepository)
        {
            var users = userRepository.List<UserDto>();
            return new OkObjectResult(users);
        }
    }
}
