using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;


namespace NexTube.Application.CQRS.Identity.Reports.Queries
{
    public class GetAllReportsFromUserQueryHandler : IRequestHandler<GetAllReportsFromUserQuery, IEnumerable<ReportLookup>>
    {
        private readonly IAdminService adminService;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public GetAllReportsFromUserQueryHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator, IAdminService iadminService)
        {
            _identityService = identityService;
            _mediator = mediator;
            adminService = iadminService;
        }
        public async Task<IEnumerable<ReportLookup>> Handle(GetAllReportsFromUserQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetAllReportsFromUser(request.UserId,request.Page,request.PageSize);
        }
    }
}
