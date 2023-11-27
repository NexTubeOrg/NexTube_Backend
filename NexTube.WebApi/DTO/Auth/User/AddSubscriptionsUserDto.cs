using AutoMapper;

using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class AddSubscriptionsUserDto : IMapWith<UpdateUserCommand>
    {

        public int? SubscriberId { get; set; }
        public int? UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddSubscriptionsUserDto, Subscription>()

                .ForMember(command => command.SubscriberId, opt => opt.MapFrom(dto => dto.SubscriberId))
                .ForMember(command => command.UserId, opt => opt.MapFrom(dto => dto.UserId));

        }
    }
}