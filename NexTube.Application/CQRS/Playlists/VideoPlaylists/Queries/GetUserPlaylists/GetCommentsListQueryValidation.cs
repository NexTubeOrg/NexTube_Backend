using FluentValidation;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists {
    public class GetUserPlaylistsQueryValidation : AbstractValidator<GetUserPlaylistsQuery> {
        public GetUserPlaylistsQueryValidation() {
            RuleFor(c => c.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
