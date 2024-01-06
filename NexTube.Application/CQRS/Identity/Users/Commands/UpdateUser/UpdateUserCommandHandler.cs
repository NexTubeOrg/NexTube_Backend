using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
        _userManager = userManager;

        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new NotFoundException("User", request.UserId.ToString());
            }
            user.Id = request.UserId;
            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;
            user.Nickname = request.Nickname ?? user.Nickname;
            user.Description = request.Description ?? user.Description ;


          await _userManager.UpdateAsync(user);
          
            return user.Id;
        }
    }

}