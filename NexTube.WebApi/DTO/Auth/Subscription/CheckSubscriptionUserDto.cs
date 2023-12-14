using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.SubscriptionUser.CheckSubscriptionUser;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.Subscription
{
    public class CheckSubscribeUserDto : IMapWith<CheckSubscriptionUserCommand>
    {

        public int SubscribeTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CheckSubscribeUserDto, CheckSubscriptionUserCommand>()

                 .ForMember(dest => dest.SubscriptionUserTo, opt => opt.MapFrom(dto => dto.SubscribeTo));


        }
    }
}