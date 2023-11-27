using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Domain.Entities;
using NexTube.WebApi.Controllers;
using NexTube.WebApi.DTO.Auth.User;
using System.Xml.Linq;

[Route("api/subscriptions")]
public class SubscriptionController : BaseController
{
    private readonly IMapper mapper;

    public SubscriptionController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    [HttpPost]
    [Route("api/subscriptions")]
    public async Task<ActionResult<SubscriptionLookup>> AddSubscription(SubscriptionsUserCommand dto)
    {
        var command = mapper.Map<SubscriptionsUserCommand>(dto);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    //[HttpDelete("unsubscribe")]
    //public ActionResult Unsubscribe(AddSubscriptionsUserDto subscriptionDto)
    //{
    //    _subscriptionService.Unsubscribe(subscriptionDto );
    //    return Ok();
    //}
}
