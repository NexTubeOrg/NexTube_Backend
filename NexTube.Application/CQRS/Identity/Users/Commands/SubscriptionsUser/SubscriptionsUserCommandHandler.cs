// Handlers/SubscribeUserToUserHandler.cs
using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;

public class SubscribeUserToUserHandler : IRequestHandler<SubscriptionsUserCommand, int>
{
    private readonly ISubscriptionsRepository _userSubscriptionService;

    public SubscribeUserToUserHandler(ISubscriptionsRepository userSubscriptionService)
    {
        _userSubscriptionService = userSubscriptionService;
    }

     public async Task<int> Handle(SubscriptionsUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userSubscriptionService.SaveAsync(request.Id, request.SubscriberId, request.TargetUserId);

        return result.UserId;
    }
}
