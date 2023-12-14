using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl;
using NexTube.Application.CQRS.Identity.Users.Commands.BanUser;
using NexTube.Application.CQRS.Identity.Users.Commands.Recover;
using NexTube.WebApi.DTO.Auth.User;

namespace NexTube.WebApi.DTO.Admin
{
    public class BanUserDto : IMapWith<BanUserCommand>
    { 
        public int UserId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BanUserDto, BanUserCommand>()
                .ForMember(command => command.UserId, opt => opt.MapFrom(dto => dto.UserId));
        }

    }
}
