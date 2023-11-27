using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.Subscriptions.Commands;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class SubscriptionUserDto : IMapWith<SubscriptionUserCommand> {

        public int SubscribeTo { get;  set; }

        public void Mapping(Profile profile) {
            profile.CreateMap< SubscriptionUserDto, SubscriptionUserCommand >()
          
                 .ForMember(dest => dest.Subscriber, opt => opt.MapFrom(dto => dto.SubscribeTo));


        }
    }
}
