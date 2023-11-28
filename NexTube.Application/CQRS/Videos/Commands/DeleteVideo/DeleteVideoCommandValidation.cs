using FluentValidation;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommandValidation : AbstractValidator<DeleteVideoCommand>
    {
        public DeleteVideoCommandValidation()
        {
            RuleFor(c => c.VideoId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
