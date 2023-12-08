using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto
{
    public class UploadPhotoValidation : AbstractValidator<UploadPhotoCommand> {
        public UploadPhotoValidation(IPhotoService photoService) {
            RuleFor(c => c.Source)
                .NotNull()
                .MustAsync(async (s, cancellation) =>
                {
                    if (!await photoService.IsFileImageAsync(s))
                        return false;

                    return true;
                })
                .WithMessage((c) => $"File must be image");
        }
    }
}
