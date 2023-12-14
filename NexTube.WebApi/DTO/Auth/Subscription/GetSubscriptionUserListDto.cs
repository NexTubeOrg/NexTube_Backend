using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.SubscriptionUser.Queries;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.Subscription
{
    public class GetSubscriptionUserDto : IMapWith<GetSubscriptionListQuery>
    {

        public int SubscribeUserTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSubscriptionUserDto, GetSubscriptionListQuery>()

                 .ForMember(dest => dest.SubscriptionUserTo, opt => opt.MapFrom(dto => dto.SubscribeUserTo));


        }
    }
}