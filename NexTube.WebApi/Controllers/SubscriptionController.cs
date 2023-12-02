using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.Subscriptions.Commands;
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
        private readonly IMediator _mediator;
        private readonly IMapper mapper;
     
        public SubscriptionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            this.mapper = mapper;
        }
        [Authorize(Roles = Roles.User)]
        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] AddSubscriptionUserDto dto)
        {
            await EnsureCurrentUserAssignedAsync();
            
            var command = mapper.Map<AddSubscriptionUserCommand>(dto);
        
            command.User = UserId;

            var result = await Mediator.Send(command);

            return Ok(result);
        }
        [HttpGet ("Subscriptions")]
        public async Task<ActionResult> GetCommentsList([FromQuery] GetSubscriptionUserDto dto)
        {
            var query = mapper.Map<GetSubscriptionQueriesCommand>(dto);
         
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = Roles.User)]
        [HttpGet("isSubscriptions")]
        public async Task<ActionResult> CheckSubscriber([FromQuery] CheckSubscribeUserDto dto)
        {
            var query = mapper.Map<CheckSubscriptionUserCommand>(dto);
            query.UserID = UserId;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = Roles.User )]
        [HttpDelete ("UnSubscribe")]
        public async Task<IActionResult> UnSubscribe([FromQuery] DeleteSubscriptionUserDto dto)
        {
            await EnsureCurrentUserAssignedAsync();
            var command = mapper.Map<DeleteSubscriptionUserCommand>(dto);
            command.User = UserId;

           var result= await Mediator.Send(command);

            return Ok(result);
        }
         
    }
}
