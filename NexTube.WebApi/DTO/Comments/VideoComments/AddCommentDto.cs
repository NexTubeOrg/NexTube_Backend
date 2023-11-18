using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl;
using NexTube.WebApi.DTO.Files.Photo;

namespace NexTube.WebApi.DTO.Comments.VideoComments
{
    public class AddCommentDto : IMapWith<AddCommentCommand>
    {
        public string Content { get; set; } = string.Empty;
        public int? VideoId { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddCommentDto, AddCommentCommand>()
                .ForMember(query => query.Content, opt => opt.MapFrom(dto => dto.Content))
                .ForMember(query => query.VideoId, opt => opt.MapFrom(dto => dto.VideoId));
        }
    }
}
