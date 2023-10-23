
using MediatR;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl
{
    public class GetPhotoUrlQuery : IRequest<GetPhotoUrlQueryVm>
    {
        public string PhotoId { get; set; } = string.Empty;
    }
}
