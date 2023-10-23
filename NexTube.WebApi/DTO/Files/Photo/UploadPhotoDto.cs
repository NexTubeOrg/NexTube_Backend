using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto;

namespace NexTube.WebApi.DTO.Files.Photo
{
    public class UploadPhotoDto : IMapWith<UploadPhotoCommand>
    {
        public IFormFile Photo { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UploadPhotoDto, UploadPhotoCommand>()
                .ForMember(command => command.Source, opt => opt.MapFrom(dto => dto.Photo.OpenReadStream()));
        }
    }
}
