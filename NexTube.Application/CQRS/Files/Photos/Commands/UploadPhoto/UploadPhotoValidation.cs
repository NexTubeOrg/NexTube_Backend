using FluentValidation;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto
{
    public class UploadPhotoValidation : AbstractValidator<UploadPhotoCommand> {
        public UploadPhotoValidation() {
            
        }
    }
}
