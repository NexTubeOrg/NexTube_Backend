using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.SubscriptionUser.DeleteSubscriptionUserCommand;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class DeleteSubscriptionUserDto : IMapWith<DeleteSubscriptionUserCommand>
    {

        public int SubscribeTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteSubscriptionUserDto, DeleteSubscriptionUserCommand>()

                 .ForMember(dest => dest.Subscriber, opt => opt.MapFrom(dto => dto.SubscribeTo));


        }
    }
}