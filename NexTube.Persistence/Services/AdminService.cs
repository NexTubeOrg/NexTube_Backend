using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.BanUser;
using NexTube.Domain.Entities;
using NexTube.Infrastructure.Services;
using NexTube.Persistence.Data.Contexts;
using Org.BouncyCastle.Asn1.Ocsp;

namespace NexTube.Persistence.Services {
    public class AdminService : IAdminService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDateTimeService _dateTimeService;
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AdminService(IDateTimeService dateTimeService, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers() {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }
        public async Task<Result> BanUser(int userId)
        {
            var user = await _dbContext.Users.Where(e => e.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));
            }
            await _userManager.RemoveFromRolesAsync(user,await _userManager.GetRolesAsync(user));
            await _userManager.AddToRoleAsync(user, "Banned");
            return Result.Success();
        }

    }
}
