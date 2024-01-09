using Microsoft.AspNetCore.SignalR;

namespace NexTube.WebApi.Hubs {
    public class NotificationsHub : Hub {
        private ILogger<NotificationsHub> _logger;

        public NotificationsHub(ILogger<NotificationsHub> logger) {
            _logger = logger;
        }

        public async Task Test(string messageFromClient) {
            _logger.LogInformation("received: " + messageFromClient);
        }
    }
}
