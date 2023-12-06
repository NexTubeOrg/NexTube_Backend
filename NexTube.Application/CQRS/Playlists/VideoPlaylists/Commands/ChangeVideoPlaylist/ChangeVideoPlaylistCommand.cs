using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ChangeVideoPlaylist {
    public class ChangeVideoPlaylistCommand : IRequest<Unit> {
        public int VideoId { get; set; }
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
    }
}
