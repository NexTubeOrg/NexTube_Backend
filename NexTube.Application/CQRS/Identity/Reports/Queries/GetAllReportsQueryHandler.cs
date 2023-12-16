using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;


namespace NexTube.Application.CQRS.Identity.Reports.Queries
{
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, IEnumerable<ReportLookup>>
    {
        private readonly IAdminService adminService;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;




        public GetAllReportsQueryHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator, IAdminService iadminService)
        {
            _identityService = identityService;
            _mediator = mediator;
            adminService = iadminService;
        }
        public async Task<IEnumerable<ReportLookup>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetAllReports(request.Page,request.PageSize);
        }
    }
}
