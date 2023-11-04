using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.ChangePassword;

namespace NexTube.WebApi.DTO.Auth.ChangePassword {
    public class ChangePasswordDto : IMapWith<ChangePasswordCommand> { 
      
        public string Password { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDto, ChangePasswordCommand>()
                .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password))
                
                .ForMember(command => command.NewPassword, opt => opt.MapFrom(dto => dto.NewPassword));
        }
    }
}
