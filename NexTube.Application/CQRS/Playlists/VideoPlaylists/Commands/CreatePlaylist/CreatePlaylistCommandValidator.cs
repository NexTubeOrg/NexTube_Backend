using FluentValidation;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist {
    public class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand> {
        public CreatePlaylistCommandValidator() {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
