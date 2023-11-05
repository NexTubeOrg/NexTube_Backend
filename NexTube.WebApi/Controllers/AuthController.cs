using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInWithProvider;
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

        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] SignInUserDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<SignInUserCommand>(dto);
            var result = await Mediator.Send(command);

            // if operation has fault
            if (result.Result.Succeeded == false)
                return UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SignInWithProviderToken([FromBody] SignInWithProviderDto dto) {
            var command = mapper.Map<SignInWithProviderCommand>(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
