using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Videos.Commands.UploadVideo
{
    public class UploadVideoCommandValidation : AbstractValidator<UploadVideoCommand>
    {
        public UploadVideoCommandValidation(IPhotoService photoService)
        {
            RuleFor(c => c.Name)
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(c => c.Description)
                .MinimumLength(2)
                .MaximumLength(1000);

            RuleFor(c => c.PreviewPhotoSource)
                .NotNull()
                .MustAsync(async (s, cancellation) =>
                {
                    if (!await photoService.IsFileImageAsync(s))
                        return false;

                    return true;
                })
                .WithMessage($"File must be photo");
        }
    }
}
