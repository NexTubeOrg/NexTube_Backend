using FluentValidation;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateChannelImage;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Identity.Users.Commands.UpdateChannelImage
{
    public class UpdateChannelImageCommandValidation : AbstractValidator<UpdateChannelImageCommand>
    {
        public UpdateChannelImageCommandValidation(IPhotoService photoService)
        {

            RuleFor(x => x.UserId)
                   .NotEmpty().WithMessage("User ID cannot be empty");

 
            RuleFor(x => x.ChannelPhotoFile)
             .NotNull()
            .MustAsync(async (s, cancellation) =>
            {
                if (!await photoService.IsFileImageAsync(s))
                    return false;
                else return true;
            }).WithMessage("Channel Photo File  mast be image");
 


        }


    }
}
