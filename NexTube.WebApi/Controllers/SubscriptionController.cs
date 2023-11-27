using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Application.Subscriptions.Commands;
using NexTube.WebApi.DTO.Auth.User;
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
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionUserDto dto)
        {
            await EnsureCurrentUserAssignedAsync();
            
            var command = mapper.Map<SubscriptionUserCommand>(dto);
            command.User = CurrentUser.Id;
         

            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }
}
