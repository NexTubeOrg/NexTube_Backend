using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.Commands.UploadVideo
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, int>
    {
        private readonly IVideoService _videoService;

        public UploadVideoCommandHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<int> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var result = await _videoService.UploadVideo(request.Name, request.Description, request.PreviewPhotoSource, request.Source, request.Creator);
            return result.VideoEntityId;
        }
    }
}
