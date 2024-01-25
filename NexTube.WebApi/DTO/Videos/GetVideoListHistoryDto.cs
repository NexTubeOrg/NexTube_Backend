using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.UserVideoHistories.Queries.GetUserVideoHistoryList;

namespace NexTube.WebApi.DTO.Videos
{
    public class GetVideoListHistoryDto : IMapWith<GetVideoListHistoryQuery>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoListHistoryDto, GetVideoListHistoryQuery>()
                .ForMember(query => query.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(query => query.PageSize, opt => opt.MapFrom(dto => dto.PageSize));
        }
    }
}
