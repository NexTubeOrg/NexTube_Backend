using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Application.Models.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NexTube.Application.Common.Interfaces
{

    public interface ISubscriptionsRepository
    {
        Task<SubscriptionsUserCommand> GetByIdAsync(int id, int targetUserId);
        Task<IEnumerable<SubscriptionsUserCommand>> GetBySubscriberIdAsync(int subscriberId);
        Task<IEnumerable<SubscriptionsUserCommand>> GetByTargetUserIdAsync(int targetUserId);
        Task<(Result Result, int UserId)> SaveAsync(SubscriptionsUserCommand subscription);
        Task DeleteAsync(SubscriptionsUserCommand subscription);

        Task AddSubscriptionAsync(int subscriberId, int targetUserId);
        Task DeleteSubscriptionAsync(int subscriberId, int targetUserId);
        Task AddSubscriptionAsync(int? subscriberId, int? targetUserId);
        Task<(Result Result, int UserId)> SaveAsync(int id, int subscriberId, int targetUserId);
    }

}

