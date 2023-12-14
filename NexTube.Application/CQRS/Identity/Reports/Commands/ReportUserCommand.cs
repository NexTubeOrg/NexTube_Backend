using MediatR;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Reports.Commands
{
    public class ReportUserCommand : IRequest<Result>
    {
        public int AbuserId { get; set; }

        public int CreatorId { get; set; }

        public Report.TypeOfReport TypeOfReport { get; set; }

        public string Body { get; set; } = string.Empty;
        public int VideoId { get; set; } 
    }
}
