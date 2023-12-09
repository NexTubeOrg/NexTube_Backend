using MediatR;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Reports.Queries
{
    public class GetAllReportsFromUserQuery : IRequest<IEnumerable<ReportLookup>>
    {
        public int UserId { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
