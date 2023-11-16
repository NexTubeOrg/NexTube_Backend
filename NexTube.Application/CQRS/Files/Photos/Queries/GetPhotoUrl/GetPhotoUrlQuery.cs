using MediatR;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl
{
    public class GetPhotoUrlQuery : IRequest<GetPhotoUrlQueryResult>
    {
        public string PhotoId { get; set; } = string.Empty;
        public int? Size { get; set; } = null;
    }
}
