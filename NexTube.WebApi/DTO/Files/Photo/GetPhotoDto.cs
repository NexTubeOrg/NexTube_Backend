using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Photos.Queries.GetPhoto;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.WebApi.DTO.Auth.User;

namespace NexTube.WebApi.DTO.Files.Photo
{
    public class GetPhotoDto : IMapWith<GetPhotoQuery>
    {
        public string PhotoId { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetPhotoDto, GetPhotoQuery>()
                .ForMember(query => query.PhotoId, opt => opt.MapFrom(dto => dto.PhotoId));
        }
    }
}
