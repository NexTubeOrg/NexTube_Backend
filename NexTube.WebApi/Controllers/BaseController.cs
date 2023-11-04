using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexTube.WebAPI.Common.Exceptions;
using NexTube.WebAPI.Filters;
using System.Security.Claims;

namespace NexTube.WebApi.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExceptionFilter]
    public abstract class BaseController : ControllerBase {
        private IMediator mediator = null!;

        // if this.mediator is null - get and set it from context.
        protected IMediator Mediator => mediator ??=
            HttpContext.RequestServices.GetService<IMediator>() ??
                throw new ServiceNotRegisteredException(nameof(IMediator));
        // get user id from claims (token).
        // if User or Identity is null - set UserId to empty
        internal int? UserId =>
            !User?.Identity?.IsAuthenticated ?? false
            ? null
            : int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");

    }
}
