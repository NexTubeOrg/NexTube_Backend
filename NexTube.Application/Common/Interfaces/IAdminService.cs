using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;


namespace NexTube.Application.Common.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<Result> BanUser(int userId);

    }
}
