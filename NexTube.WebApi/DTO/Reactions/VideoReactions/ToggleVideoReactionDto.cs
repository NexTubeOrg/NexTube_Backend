using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Reactions.VideoReactions.Commands.SetReaction;
using NexTube.Domain.Entities;

namespace NexTube.WebApi.DTO.Reactions.VideoReactions {
    public class ToggleVideoReactionDto : IMapWith<SetReactionCommand> {
        public int VideoId { get; set; }
        public VideoReactionEntity.VideoReactionType ReactionType { get; set; }
        public void Mapping(Profile profile) {
            profile.CreateMap<ToggleVideoReactionDto, SetReactionCommand>()
                .ForMember(r => r.ReactedVideoId, opt => opt.MapFrom(dto => dto.VideoId))
                .ForMember(r => r.ReactionType, opt => opt.MapFrom(dto => dto.ReactionType));
        }
    }
}
