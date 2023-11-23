
using MediatR;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;



namespace NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser
{
    public class SubscriptionsUserCommand : IRequest<int>
    {
    
        public int Id { get; set; }
   
            public int SubscriberId { get; set; }
            public int TargetUserId { get; set; }

            public int GetId()
            {
                return SubscriberId;
            }
         
    }
}