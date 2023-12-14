using MediatR;

namespace NexTube.Application.CQRS.Identity.Reports.Commands
{
    public class RemoveReportByEntityIdCommand : IRequest
    {
        public int ReportId { get; set; } = 0;
    }
}
