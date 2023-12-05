using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.GetUser;
using NexTube.Application.Subscriptions.Commands;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Auth.Subscription
{
    public class GetUserDto : IMapWith<GetUserCommand>
    {

        public int ChannelId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetUserDto, GetUserCommand>()

                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(dto => dto.ChannelId));


        }
    }
}
