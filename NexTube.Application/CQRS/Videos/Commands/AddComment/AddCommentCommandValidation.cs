using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Videos.Commands.AddComment {
    public class AddCommentCommandValidation : AbstractValidator<AddCommentCommand> {
        public AddCommentCommandValidation() {
            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(500);

            RuleFor(c => c.VideoId)
                .NotEmpty();
        }
    }
}
