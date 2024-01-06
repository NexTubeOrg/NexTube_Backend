using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Identity.Users.Commands.UpdateChannelImage
{
    public class UpdateChannelImageCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public Stream ChannelPhotoFile{ get; set; } = null!;
    }
}
