using Microsoft.AspNetCore.SignalR;

namespace NexTube.Persistence.Data.Providers {
    public class ApplicationUserIdProvider : IUserIdProvider {
        public string? GetUserId(HubConnectionContext connection) {
            return connection.User?.FindFirst("userId")?.Value;
        }
    }
}
