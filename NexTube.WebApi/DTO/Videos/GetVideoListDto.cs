﻿using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.CQRS.Videos.Queries.GetVideoList;
using NexTube.WebApi.DTO.Comments.VideoComments;

namespace NexTube.WebApi.DTO.Videos
{
    public class GetVideoListDto : IMapWith<GetVideoListQuery>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoListDto, GetVideoListQuery>()
                .ForMember(query => query.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(query => query.PageSize, opt => opt.MapFrom(dto => dto.PageSize));
        }
    }
}
