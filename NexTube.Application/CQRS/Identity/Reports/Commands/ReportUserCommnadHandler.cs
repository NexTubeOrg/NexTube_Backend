using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;


namespace NexTube.Application.CQRS.Identity.Reports.Commands
{
    public class ReportUserCommnadHandler : IRequestHandler<ReportUserCommand, Result>
    {
        private readonly IAdminService adminService;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;




        public ReportUserCommnadHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator, IAdminService iadminService)
        {
            _identityService = identityService;
            _mediator = mediator;
            adminService = iadminService;
        }
        public async Task<Result> Handle(ReportUserCommand request, CancellationToken cancellationToken)
        {
            return await adminService.ReportUser(request.CreatorId,request.AbuserId,request.VideoId,request.TypeOfReport,request.Body) ;
        }
    }
}
