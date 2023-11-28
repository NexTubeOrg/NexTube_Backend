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
        }
    }
}
