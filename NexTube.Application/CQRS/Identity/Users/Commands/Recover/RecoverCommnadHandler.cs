using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;

namespace NexTube.Application.CQRS.Identity.Users.Commands.Recover { 

    public class RecoverCommnadHandler : IRequestHandler<RecoverCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public RecoverCommnadHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(RecoverCommand request, CancellationToken cancellationToken)
        {
             var result = await _identityService.RecoverAsync(
                request.Email);


            return Result.Success();
        }
    }
}
