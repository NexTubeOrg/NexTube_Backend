using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl
{
    public class GetPhotoUrlQueryValidation : AbstractValidator<GetPhotoUrlQuery>
    {
        public GetPhotoUrlQueryValidation(IPhotoService photoService)
        {
            RuleFor(q => q.PhotoId)
                .NotEmpty();
        }
    }
}
