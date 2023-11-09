using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl;
using NexTube.Application.CQRS.Videos.Commands.AddComment;
using NexTube.WebApi.DTO.Files.Photo;

namespace NexTube.WebApi.DTO.Videos {
    public class AddCommentDto : IMapWith<AddCommentCommand> {
        public string Content { get; set; } = string.Empty;
        public int? VideoId { get; set; } = null;

        public void Mapping(Profile profile) {
            profile.CreateMap<AddCommentDto, AddCommentCommand>()
                .ForMember(query => query.Content, opt => opt.MapFrom(dto => dto.Content))
                .ForMember(query => query.VideoId, opt => opt.MapFrom(dto => dto.VideoId));
        }
    }
}
