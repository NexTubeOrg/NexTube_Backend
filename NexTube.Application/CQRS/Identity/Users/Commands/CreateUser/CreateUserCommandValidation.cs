using FluentValidation;

namespace NexTube.Application.CQRS.Identity.Users.Commands.CreateUser
{
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64)
                .Equal(u=>u.PasswordConfirm).WithMessage("Passwords does not matches");
               
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(128);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(128);

            RuleFor(c => c.Email)
                .NotEmpty()
                .Matches("[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        
          //  RuleFor(x => x.Nickname).NotEmpty()
          //       .NotEmpty().WithMessage("Нікнейм є обов'язковим.")
          //       .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Некоректний формат нікнейму. Дозволені лише літери та цифри.");

          //  RuleFor(x => x.Description)
          //  .NotEmpty()
          //.MaximumLength(500)
          //.WithMessage("Максимальна довжина опису каналу - 500 символів.");
          



        }
    }
}
