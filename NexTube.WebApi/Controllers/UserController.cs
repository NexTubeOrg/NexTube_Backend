using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.ChangeBanner;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.WebApi.DTO.Auth.User;
using NexTube.WebApi.DTO.User;
using WebShop.Domain.Constants;

namespace NexTube.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Authorize(Roles = Roles.User)]
        [HttpPut]
        public async Task<ActionResult> ChangeBanner([FromForm] ChangeBannerDto dto)
        {
            await EnsureCurrentUserAssignedAsync();

            var command = mapper.Map<ChangeBannerCommand>(dto);
            command.Requester = CurrentUser;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
