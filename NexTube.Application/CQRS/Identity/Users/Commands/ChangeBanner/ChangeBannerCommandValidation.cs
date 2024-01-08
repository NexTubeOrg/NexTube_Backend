using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangeBanner
{
    public class ChangeBannerCommandValidation : AbstractValidator<ChangeBannerCommand>
    {
        public ChangeBannerCommandValidation(IPhotoService photoService)
        {
            int width = -1, height = -1;
            RuleFor(c => c.BannerStream)
                .NotNull()
                .MustAsync(async (s, cancellation) => {
                    if (!await photoService.IsFileImageAsync(s))
                        return false;

                    var dimensions = await photoService.GetPhotoDimensionsAsync(s);
                    width = dimensions.Dimensions.Width;
                    height = dimensions.Dimensions.Height;
                    return Math.Abs((double)width / height - 16.0 / 9.0) < 0.01;
                }).WithMessage((c) => $"File must be image and photo must have 16x9 ratio aspect, your dimensions: width:{width}, height:{height}");
        }
    }
}
