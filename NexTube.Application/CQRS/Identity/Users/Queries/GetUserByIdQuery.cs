using MediatR;
using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Identity.Users.Queries {
    public class GetUserByIdQuery : IRequest<ApplicationUser> {
        public int UserId { get; set; }
    }
}
