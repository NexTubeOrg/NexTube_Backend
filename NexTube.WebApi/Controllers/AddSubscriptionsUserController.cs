using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.Common.Interfaces;
using NexTube.WebApi.DTO.Auth.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NexTube.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddSubscriptionsUserController : BaseController
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public AddSubscriptionsUserController(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository ?? throw new ArgumentNullException(nameof(subscriptionsRepository));
        }

        [HttpPost]
        public async Task<IActionResult> AddSubscription([FromBody] AddSubscriptionsUserDto subscriptionDto)
        {
            try
            {
                Guard.Against.Null(subscriptionDto, nameof(subscriptionDto));

                await _subscriptionsRepository.AddSubscriptionAsync(subscriptionDto.SubscriberId, subscriptionDto.TargetUserId);

                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
