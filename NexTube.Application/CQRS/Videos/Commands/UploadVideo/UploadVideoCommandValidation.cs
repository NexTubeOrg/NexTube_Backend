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


            int width = -1, height = -1;
            RuleFor(c => c.PreviewPhotoSource)
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
