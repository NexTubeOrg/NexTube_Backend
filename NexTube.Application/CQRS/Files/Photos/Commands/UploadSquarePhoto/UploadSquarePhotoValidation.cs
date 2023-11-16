using FluentValidation;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadSquarePhoto {
    public class UploadSquarePhotoValidation : AbstractValidator<UploadSquarePhotoCommand> {
        public UploadSquarePhotoValidation(IPhotoService photoService) {
            int width = 1, height = 1;
            RuleFor(c=>c.Source)
                // ensure that uploaded photo has 1x1 ratio
                .MustAsync(async(s, cancellation) => {
                    var dimensions = await photoService.GetPhotoDimensionsAsync(s);
                    s.Position = 0; // reset stream pointer position
                    width = dimensions.Dimensions.Width;
                    height = dimensions.Dimensions.Height;
                    return width == height;
                }).WithMessage((c)=> $"Photo must have 1x1 ratio aspect, your dimensions: width:{width}, height:{height}");
        }
    }
}
