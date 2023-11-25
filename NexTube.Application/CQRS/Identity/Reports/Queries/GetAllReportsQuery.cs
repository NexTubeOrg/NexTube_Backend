using MediatR;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Reports.Queries
{
    public class GetAllReportsQuery : IRequest<IEnumerable<ReportLookup>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
