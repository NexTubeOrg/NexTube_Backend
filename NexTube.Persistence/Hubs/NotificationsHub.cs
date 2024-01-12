using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebShop.Domain.Constants;

namespace NexTube.Infrastructure.Hubs {
    [Authorize(Roles = Roles.User)]
    public class NotificationsHub : Hub {
        private ILogger<NotificationsHub> _logger;

        public NotificationsHub(ILogger<NotificationsHub> logger) {
            _logger = logger;
        }
    }
}
