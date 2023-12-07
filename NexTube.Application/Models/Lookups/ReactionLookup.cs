using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.Models.Lookups {
    public class ReactionLookup : IMapWith<VideoReactionEntity> {
        public VideoReactionEntity.VideoReactionType ReactionType { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<VideoReactionEntity, ReactionLookup>()
                .ForMember(l => l.ReactionType, opt => opt.MapFrom(e => e.Type));
        }
    }
}
