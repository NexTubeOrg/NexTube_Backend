using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateChannelImage;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class UpdateChannelImageDto : IMapWith<UpdateChannelImageCommand>
    {
        public IFormFile ChannelPhotoFile { get; set; } = null!;
      

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateChannelImageDto, UpdateChannelImageCommand>()
                 .ForMember(command => command.ChannelPhotoFile, opt => opt.MapFrom(dto => dto.ChannelPhotoFile.OpenReadStream()));
               

        }
    }
}
