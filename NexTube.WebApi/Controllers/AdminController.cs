using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities;
using NexTube.Application.CQRS.Identity.Users.Commands.BanUser;
using NexTube.Application.CQRS.Identity.Users.Queries;
using NexTube.WebApi.DTO.Admin;
using NexTube.WebApi.DTO.Auth.User;
using WebShop.Domain.Constants;

namespace NexTube.WebApi.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IMapper mapper;

        public AdminController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Authorize(Roles =  Roles.Administrator+"," + Roles.Moderator)]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var getAllUsersQueryResult = await Mediator.Send(query);

            return Ok(getAllUsersQueryResult);
        }
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        [HttpPost]
        public async Task<ActionResult> BanUser([FromBody] BanUserDto dto)
        {
            var command = mapper.Map<BanUserCommand>(dto);
            var result = await Mediator.Send(command);
            if (result.Result.Succeeded == false)
                return UnprocessableEntity(result);

            return Ok(result);
        }

    }
}
