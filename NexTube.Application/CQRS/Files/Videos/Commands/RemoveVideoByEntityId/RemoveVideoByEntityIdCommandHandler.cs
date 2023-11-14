using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.Commands.RemoveVideoByEntityId
{
    public class RemoveVideoByEntityIdCommandHandler : IRequestHandler<RemoveVideoByEntityIdCommand>
    {
        private readonly IVideoService _videoService;

        public RemoveVideoByEntityIdCommandHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task Handle(RemoveVideoByEntityIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _videoService.RemoveVideoByEntityId(request.VideoEntityId);
        }
    }
}
