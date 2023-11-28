using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.Subscriptions.Commands;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.Subscription
{
    public class GetSubscriptionUserDto : IMapWith<GetSubscriptionQueriesCommand>
    {

        public int SubscribeUserTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSubscriptionUserDto, GetSubscriptionQueriesCommand>()

                 .ForMember(dest => dest.SubscriptionUserTo, opt => opt.MapFrom(dto => dto.SubscribeUserTo));


        }
    }
}
