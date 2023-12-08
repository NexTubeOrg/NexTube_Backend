using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddCommentReply;

namespace NexTube.WebApi.DTO.Comments.VideoComments {
    public class AddCommentReplyDto : IMapWith<AddCommentReplyCommand> {
        public string Content { get; set; } = string.Empty;
        public int? VideoId { get; set; } = null;
        public int? RootCommentId { get; set; } = null;

        public void Mapping(Profile profile) {
            profile.CreateMap<AddCommentReplyDto, AddCommentReplyCommand>()
                .ForMember(query => query.Content, opt => opt.MapFrom(dto => dto.Content))
                .ForMember(query => query.VideoId, opt => opt.MapFrom(dto => dto.VideoId))
                .ForMember(query => query.ReplyToCommentId, opt => opt.MapFrom(dto => dto.RootCommentId));
        }
    }
}
