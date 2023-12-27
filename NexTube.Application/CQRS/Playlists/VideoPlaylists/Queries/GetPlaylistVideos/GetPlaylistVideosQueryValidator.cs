using FluentValidation;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetPlaylistVideos {
    public class GetPlaylistVideosQueryValidator : AbstractValidator<GetPlaylistVideosQuery> {
        public GetPlaylistVideosQueryValidator() {
            RuleFor(c => c.PlaylistId)
                .NotEmpty();

            RuleFor(c => c.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
