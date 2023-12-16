using FluentValidation;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidation() {

            RuleFor(x => x.UserId)
                   .NotEmpty().WithMessage("User ID cannot be empty");

            RuleFor(x => x.FirstName)
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

            RuleFor(x => x.LastName)
              .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");

            RuleFor(x => x.Nickname)
                .MinimumLength(2).WithMessage("Nickname must be at least 2 characters long")
                .MaximumLength(30).WithMessage("Nickname cannot be longer than 30 characters");

            RuleFor(x => x.Description)
                .MinimumLength(2).WithMessage("Description must be at least 10 characters long")
                .MaximumLength(1000).WithMessage("Description cannot be longer than 1000 characters");




        }


    }
}
