using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangeBanner
{
    public class ChangeBannerCommandValidation : AbstractValidator<ChangeBannerCommand>
    {
        public ChangeBannerCommandValidation(IPhotoService photoService)
        {
            RuleFor(c => c.BannerStream)
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
