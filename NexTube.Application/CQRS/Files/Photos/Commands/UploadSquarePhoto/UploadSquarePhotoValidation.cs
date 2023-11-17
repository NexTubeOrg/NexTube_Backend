using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadSquarePhoto {
    public class UploadSquarePhotoValidation : AbstractValidator<UploadSquarePhotoCommand> {
        public UploadSquarePhotoValidation(IPhotoService photoService) {
            int width = -1, height = -1;
            RuleFor(c=>c.Source)
                .NotNull()
                // ensure that uploaded photo has 1x1 ratio
                .MustAsync(async(s, cancellation) => {
                    if (!await photoService.IsFileImageAsync(s))
                        return false;

                    var dimensions = await photoService.GetPhotoDimensionsAsync(s);
                    width = dimensions.Dimensions.Width;
                    height = dimensions.Dimensions.Height;
                    return width == height;
                }).WithMessage((c)=> $"File must be image and photo must have 1x1 ratio aspect, your dimensions: width:{width}, height:{height}");
        }
    }
}
