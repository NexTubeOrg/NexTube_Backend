using Microsoft.AspNetCore.Mvc;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.WebApi.Controllers;
using System.Xml.Linq;

[Route("api/subscriptions")]
public class SubscriptionController : BaseController
{
    private readonly ISubscriptionsRepository _subscriptionService;

    public SubscriptionController(ISubscriptionsRepository subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("{userId}", Name = "GetSubscriptions")]
    public ActionResult<IEnumerable<Subscription>> GetSubscriptions(int userId)
    {
        var subscriptions = _subscriptionService.GetSubscriptions(userId);
        return Ok(subscriptions.Select(x => new Subscription(x)));
    }

    [HttpPost("subscribe")]
    public ActionResult Subscribe(Subscription subscriptionDto)
    {
        _subscriptionService.Subscribe(subscriptionDto );
        return Ok();
    }

    [HttpDelete("unsubscribe")]
    public ActionResult Unsubscribe(Subscription subscriptionDto)
    {
        _subscriptionService.Unsubscribe(subscriptionDto );
        return Ok();
    }
}
