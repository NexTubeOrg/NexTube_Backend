using AutoMapper;

using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class AddSubscriptionsUserDto : IMapWith<UpdateUserCommand>
    {

        public int? SubscriberId { get; set; }
        public int? TargetUserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddSubscriptionsUserDto, SubscriptionsUserCommand>()

                .ForMember(command => command.SubscriberId, opt => opt.MapFrom(dto => dto.SubscriberId))
                .ForMember(command => command.TargetUserId, opt => opt.MapFrom(dto => dto.TargetUserId));

        }
    }
}