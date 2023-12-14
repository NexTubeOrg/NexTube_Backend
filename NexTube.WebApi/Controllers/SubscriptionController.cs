using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.CQRS.SubscriptionUser.AddSubscriptionUser;
using NexTube.Application.CQRS.SubscriptionUser.CheckSubscriptionUser;
using NexTube.Application.CQRS.SubscriptionUser.DeleteSubscriptionUserCommand;
using NexTube.Application.CQRS.SubscriptionUser.Queries;
using NexTube.WebApi.DTO.Auth.Subscription;
using NexTube.WebApi.DTO.Auth.User;
using NexTube.WebApi.DTO.Comments.VideoComments;
using System.Security.Principal;
using WebShop.Domain.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NexTube.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : BaseController
    {
       
        private readonly IMapper mapper;

        public SubscriptionController( IMapper mapper)
        {
         
            this.mapper = mapper;
        }
        [Authorize(Roles = Roles.User)]
        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] AddSubscriptionUserDto dto)
        {
            await EnsureCurrentUserAssignedAsync();

            var command = mapper.Map<AddSubscriptionUserCommand>(dto);

            command.User = CurrentUser.Id;

            var result = await Mediator.Send(command);

            return Ok(result);
        }
        [Authorize(Roles = Roles.User)]
        [HttpGet("Subscriptions")]
        public async Task<ActionResult> GetSubscribeList( )
        { 
            var query = new GetSubscriptionListQuery
            {
                SubscriptionUserTo = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = Roles.User)]
        [HttpGet("isSubscriptions")]
        public async Task<ActionResult> CheckSubscriber([FromQuery] CheckSubscribeUserDto dto)
        {
            await EnsureCurrentUserAssignedAsync();
            var query = mapper.Map<CheckSubscriptionUserCommand>(dto);
            query.UserID = CurrentUser.Id;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = Roles.User)]
        [HttpDelete("UnSubscribe")]
        public async Task<IActionResult> UnSubscribe([FromQuery] DeleteSubscriptionUserDto dto)
        {
            await EnsureCurrentUserAssignedAsync();
            var command = mapper.Map<DeleteSubscriptionUserCommand>(dto);
            command.User = CurrentUser.Id;

            var result = await Mediator.Send(command);

            return Ok(result);
        }

    }
}