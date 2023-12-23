using MediatR;


namespace NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}