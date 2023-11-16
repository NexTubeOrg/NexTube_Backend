using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment;

namespace NexTube.WebApi.DTO.Comments.VideoComments
{
    public class DeleteCommentDto : IMapWith<DeleteCommentCommand>
    {
        public int? Id { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCommentDto, DeleteCommentCommand>()
                .ForMember(query => query.CommentId, opt => opt.MapFrom(dto => dto.Id));
        }
    }
}
