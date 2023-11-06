using FluentValidation;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangePassword {
    public class ChangePasswordCommandValidation : AbstractValidator<ChangePasswordCommand> {
        public ChangePasswordCommandValidation() {
            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);

            RuleFor(c => c.NewPassword)
               .NotEmpty()
               .MinimumLength(8)
               .MaximumLength(64);
        }
    }
}
