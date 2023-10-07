using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.WebApi.DTO.Auth.User;

namespace NexTube.WebApi.Controllers {
    public class AuthController : BaseController {

        private readonly IMapper mapper;
        public AuthController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SignUp([FromForm] SignUpDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<CreateUserCommand>(dto);
            var userId = await Mediator.Send(command);

            return Ok(userId);
        }
    }
}
