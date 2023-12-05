
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Domain.Entities;


namespace NexTube.Application.CQRS.Identity.Users.Commands.GetUser
{
    public class GetUserCommnadHandler : IRequestHandler<GetUserCommand, GetUserCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserCommnadHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context=context;
        }

        public async Task<GetUserCommandResponse> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            // Замініть "User" та інші назви відповідно до вашої моделі користувача та контексту бази даних
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());


            if (user == null)
            {
                // Можна викинути виняток чи повернути null в залежності від ваших потреб
                throw new NotFoundException(request.UserId.ToString(), nameof(ApplicationUser));
            }

            return new GetUserCommandResponse
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Nickname = user.Nickname,
                Description = user.Description,
                Subsciptions = await _context.Subscriptions
    .Where(s => s.Subscriber.Id == request.UserId)
    .CountAsync(),
                Video= await _context.Videos
    .Where(s => s.Creator.Id == request.UserId)
    .CountAsync(),
                ChannelPhotoFileId = Guid.Parse(user.ChannelPhotoFileId.ToString()),
                // Додайте інші поля, які вам потрібні
            };

        }
    }
}