using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.GetChannelInfo;


namespace NexTube.WebApi.DTO.Auth.Subscription
{
    public class GetChannelInfoDto : IMapWith<GetChannelInfoCommand>
    {

        public int ChannelId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetChannelInfoDto, GetChannelInfoCommand>()

                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(dto => dto.ChannelId));


        }
    }
}