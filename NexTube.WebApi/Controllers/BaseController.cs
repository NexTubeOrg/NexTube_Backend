using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexTube.WebAPI.Common.Exceptions;
using NexTube.WebAPI.Filters;

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

    }
}
