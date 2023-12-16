using FluentValidation;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists {
    public class GetUserPlaylistsQueryValidator : AbstractValidator<GetUserPlaylistsQuery> {
        public GetUserPlaylistsQueryValidator() {
            RuleFor(c => c.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
