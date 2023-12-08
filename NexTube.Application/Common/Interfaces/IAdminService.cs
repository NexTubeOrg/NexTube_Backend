using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;


namespace NexTube.Application.Common.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers(int page, int pageSize);
        Task<Result> BanUser(int userId);
        Task<Result> ReportUser(int creatorId,int abuserId,int videoId, Report.TypeOfReport typeOfReport, string body);
        Task<IEnumerable<ReportLookup>> GetAllReports(int page,int pageSize);
        Task<IEnumerable<ReportLookup>> GetAllReportsFromUser(int userId, int page, int pageSize);

        Task<Result> AssignModerator(int userId);

        Task<Result> RemoveModerator(int userId);

        Task<Result> RemoveReportById(int id);
    }
}
