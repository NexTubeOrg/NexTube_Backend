using FluentValidation;

namespace NexTube.Application.CQRS.Identity.Users.Commands.Recover {
    public class RecoverCommandValidation : AbstractValidator<RecoverCommand> {
        public RecoverCommandValidation() {
            RuleFor(c => c.Email)
                .NotEmpty()
                .Matches("[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        }
    }
}
