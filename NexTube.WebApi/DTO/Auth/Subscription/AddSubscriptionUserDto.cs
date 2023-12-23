using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.SubscriptionUser.AddSubscriptionUser;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.Subscription
{
    public class AddSubscriptionUserDto : IMapWith<AddSubscriptionUserCommand>
    {

        public int SubscribeTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddSubscriptionUserDto, AddSubscriptionUserCommand>()

                 .ForMember(dest => dest.Subscriber, opt => opt.MapFrom(dto => dto.SubscribeTo));


        }
    }
}