using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.BanUser;
using NexTube.Application.Models.Lookups;
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
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers(int page, int pageSize) {
            if (page < 1 || pageSize < 1 || (page - 1) * pageSize > await _userManager.Users.CountAsync())
            {
                throw new ArgumentException("Invalid page or pageSize values");
            }
            var users = await _dbContext.UserRoles.Where(c => c.RoleId == _dbContext.Roles.Where(a => a.Name == "User").First().Id)
                .Join(_dbContext.Users,
                    arg => arg.UserId, 
                    arg => arg.Id,       
                    (userRole,user) => user)
           .OrderBy(u => u.Id)
           .Skip((page - 1) * pageSize)
           .Take(pageSize).ToListAsync();


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
        public async Task<Result> AssignModerator(int userId)
        {
            var user = await _dbContext.Users.Where(e => e.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));
            }
            if(! await _userManager.IsInRoleAsync(await _userManager.Users.Where(c => c.Id == userId).FirstAsync(),"Moderator"))
            await _userManager.AddToRoleAsync(user, "Moderator");
            else
            await _userManager.RemoveFromRoleAsync(user, "Moderator");

            return Result.Success();
        }
        public async Task<Result> RemoveModerator(int userId)
        {
            var user = await _dbContext.Users.Where(e => e.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));
            }
            await _userManager.RemoveFromRolesAsync(user, new List<string>() { "Moderator" });

            return Result.Success();
        }

        public async Task<Result> ReportUser(int creatorId,int abuserId,int videoId,Report.TypeOfReport typeOfReport, string body)
        {
            var abuser = await _dbContext.Users.Where(e => e.Id == abuserId).FirstOrDefaultAsync();
  

            var creator = await _dbContext.Users.Where(e => e.Id == creatorId).FirstOrDefaultAsync();

            var video = await _dbContext.Videos.Where(e => e.Id == videoId).FirstOrDefaultAsync();

            if (abuser == null)
            {
                throw new NotFoundException(abuserId.ToString(), nameof(ApplicationUser));
            }
            if (creator == null)
            {
                throw new NotFoundException(creatorId.ToString(), nameof(ApplicationUser));
            }

            _dbContext.Reports.Add(new Report() { Creator = creator, Abuser = abuser, Body = body, Type = typeOfReport,Video = video  });
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<IEnumerable<ReportLookup>> GetAllReports(int page,int pageSize)
        {
            var reports = _dbContext.Reports
                .OrderByDescending(c => c.DateCreated)
            .Include(c => c.Creator)
            .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ReportLookup()
                {
                    Type = c.Type,
                    Body = c.Body,
                    DateCreated = c.DateCreated,
                    Abuser = new UserLookup()
                    {
                        UserId = c.Abuser.Id,
                        FirstName = c.Abuser.FirstName,
                        LastName = c.Abuser.LastName,
                        ChannelPhoto = c.Abuser.ChannelPhotoFileId.ToString(),
                        Email = c.Abuser.Email,
                    },
                    Creator = new UserLookup()
                    {
                        UserId = c.Creator.Id,
                        FirstName = c.Creator.FirstName,
                        LastName = c.Creator.LastName,
                        ChannelPhoto = c.Creator.ChannelPhotoFileId.ToString(),
                        Email = c.Abuser.Email,
                    },
                    Id = c.Id,
                    VideoId = c.Video.Id

                });


            return await reports.ToListAsync();
        }

        public async Task<IEnumerable<ReportLookup>> GetAllReportsFromUser(int abuserId, int page, int pageSize)
        {
            var abuser = await _dbContext.Users.Where(e => e.Id == abuserId).FirstOrDefaultAsync();
            if (abuser == null)
            {
                throw new NotFoundException(abuserId.ToString(), nameof(ApplicationUser));
            }
            var reports = _dbContext.Reports
                .Where(c => c.Abuser.Id == abuserId)
                .OrderByDescending(c => c.DateCreated)
                .Include(c => c.Creator)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ReportLookup()
                {
                    Type = c.Type,
                    Body = c.Body,
                    DateCreated = c.DateCreated,
                    Abuser = new UserLookup()
                    {
                        UserId = c.Abuser.Id,
                        FirstName = c.Abuser.FirstName,
                        LastName = c.Abuser.LastName,
                        ChannelPhoto = c.Abuser.ChannelPhotoFileId.ToString()
                    },
                    Creator = new UserLookup()
                    {
                        UserId = c.Creator.Id,
                        FirstName = c.Creator.FirstName,
                        LastName = c.Creator.LastName,
                        ChannelPhoto = c.Creator.ChannelPhotoFileId.ToString()
                    },
                    Id = c.Id,
                    VideoId = c.Video.Id

                }); ;
            return await reports.ToListAsync();        }

        public async Task<Result> RemoveReportById(int reportId) {
            var report = await _dbContext.Reports.Where(c => c.Id == reportId).FirstOrDefaultAsync();
            if (report == null)
            {
                throw new NotFoundException(reportId.ToString(), nameof(Report));
            }
            _dbContext.Reports.Remove(report);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }


    }
}
