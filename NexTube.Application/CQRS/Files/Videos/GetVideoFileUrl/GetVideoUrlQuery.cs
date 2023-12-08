using MediatR;

namespace NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl
{
    public class GetVideoUrlQuery : IRequest<GetVideoUrlQueryResult>
    {
        public string VideoFileId { get; set; } = string.Empty;
    }
}
