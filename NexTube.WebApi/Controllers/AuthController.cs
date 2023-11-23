﻿using AutoMapper;
using Google.Apis.Auth;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.ChangePassword;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.Recover;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInWithProvider;
using NexTube.WebApi.DTO.Auth.ChangePassword;
using NexTube.WebApi.DTO.Auth.User;
using WebShop.Domain.Constants;

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

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SignInWithProviderToken([FromBody] SignInWithProviderDto dto) {
            var command = mapper.Map<SignInWithProviderCommand>(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Recover([FromBody] RecoverDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<RecoverCommand>(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto) {
            // map received from request dto to cqrs command
            var command = mapper.Map<ChangePasswordCommand>(dto);
            command.UserId = UserId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
