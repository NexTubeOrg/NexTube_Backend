using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommandValidation : AbstractValidator<UpdateVideoCommand>
    {
        public UpdateVideoCommandValidation()
        {
            RuleFor(c => c.VideoId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.Name)
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(c => c.Description)
                .MinimumLength(2)
                .MaximumLength(1000);
        }
    }
}
