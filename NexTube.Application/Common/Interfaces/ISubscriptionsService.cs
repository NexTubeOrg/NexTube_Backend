using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NexTube.Application.Common.Interfaces
{

    public interface ISubscriptionsService
    {

        //Task<(Result Result, int UserId)> SaveAsync(SubscriptionsUserCommand subscription);
        //Task<(Result Result, ApplicationUser User)> GetByIdAsync(int userId);
        //Task DeleteSubscriptionAsync(int subscriberId, int targetUserId);
        //Task SubscribeAsync(int userId, int subscriberId);
        //Task UnsubscribeAsync(int userId, int subscriberId);
        //IEnumerable<SubscriptionsUserCommand> GetSubscriptions(int userId);
        //void Unsubscribe(int userId, int subscriberId);
        //      void Subscribe(int userId, int subscriberId );


        IEnumerable<SubscriptionEntity> GetSubscriptions(int userId);
  
        Task<Result> Subscribe(SubscriptionEntity subscription);

        Task<Result> Unsubscribe(SubscriptionEntity subscription);
    }

}

