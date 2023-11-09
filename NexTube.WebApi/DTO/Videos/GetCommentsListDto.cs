using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Videos.Commands.AddComment;
using NexTube.Application.CQRS.Videos.Queries.GetCommentsList;

namespace NexTube.WebApi.DTO.Videos {
    public class GetCommentsListDto : IMapWith<GetCommentsListQuery> {
        public int VideoId { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<GetCommentsListDto, GetCommentsListQuery>()
                .ForMember(query => query.VideoId, opt => opt.MapFrom(dto => dto.VideoId));
        }
    }
}
