using NexTube.Application.Common.Interfaces;

namespace NexTube.Infrastructure.Services;

public class DateTimeService : IDateTimeService {
    public DateTime Now => DateTime.UtcNow;
}
