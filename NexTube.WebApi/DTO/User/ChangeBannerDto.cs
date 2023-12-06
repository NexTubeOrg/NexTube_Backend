using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto;
using NexTube.Application.CQRS.Identity.Users.Commands.ChangeBanner;
using NexTube.WebApi.DTO.Files.Photo;

namespace NexTube.WebApi.DTO.User
{
    public class ChangeBannerDto : IMapWith<ChangeBannerCommand>
    {
        public IFormFile? Banner { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeBannerDto, ChangeBannerCommand>()
                .ForMember(command => command.BannerStream, opt => opt.MapFrom(dto => dto.Banner.OpenReadStream()));
        }
    }
}
