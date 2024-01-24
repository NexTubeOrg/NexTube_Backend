using MediatR;

namespace NexTube.Application.Common.Interfaces {
    public interface IEventPublisher {
        Task SendEvent(object data);
    }
}
