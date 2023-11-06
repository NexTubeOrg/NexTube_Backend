using MediatR;
using NexTube.Application.Common.Interfaces;


namespace NexTube.Application.CQRS.Files.Videos.Commands.UploadVideo
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, string>
    {
        private readonly IVideoService _videoService;

        public UploadVideoCommandHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<string> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var result = await _videoService.UploadVideo(request.Name, request.Description, request.PreviewPhotoSource, request.Source);
            return result.VideoId;
        }
    }
}
