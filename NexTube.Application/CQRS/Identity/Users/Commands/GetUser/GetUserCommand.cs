using MediatR;
 
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Identity.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResult>
    {
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Nickname { get; set; } = null;
        public string Description { get; set; }=null;
     
        public Stream ChannelPhotoStream { get; set; } = null!;
    }
}
     