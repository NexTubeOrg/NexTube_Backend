﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.GetChannelInfo;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateChannelImage;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser;
using NexTube.WebApi.DTO.Auth.Subscription;
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

        [HttpGet]
        public async Task<ActionResult> GetUser([FromQuery] GetChannelInfoDto dto)
        {

            var command = mapper.Map<GetChannelInfoCommand>(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPut]
        public async Task<ActionResult> UpdateChannelImage([FromForm] UpdateChannelImageDto dto)
        {
            var command = mapper.Map<UpdateChannelImageCommand>(dto);
            command.UserId = (int)UserId;
            await Mediator.Send(command);

            return NoContent();
        }

    }
}