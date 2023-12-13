using AutoMapper;

using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Nickname { get; set; } = null!;
        public string? Description { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                 .ForMember(command => command.FirstName, opt => opt.MapFrom(dto => dto.FirstName))
                 .ForMember(command => command.LastName, opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(command => command.Nickname, opt => opt.MapFrom(dto => dto.Nickname))
                .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));

        }
    }
}