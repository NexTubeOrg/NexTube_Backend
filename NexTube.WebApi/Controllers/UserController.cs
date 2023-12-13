using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser;
using NexTube.WebApi.DTO.Auth.User;
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
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto dto)  
        {

       
            var command = mapper.Map<UpdateUserCommand>(dto);
            command.UserId = (int)UserId; 
            await Mediator.Send(command);

            return NoContent();  
        }


    }
}