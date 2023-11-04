using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser;
using NexTube.WebApi.DTO.Auth.User;

namespace NexTube.WebApi.Controllers
{
  
    public class UserController : BaseController
    {
        private readonly IMapper mapper;
        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [HttpPut] // Зміни POST на PUT для редагування
        public async Task<ActionResult> UpdateUser( [FromBody] UpdateUserDto dto) // Використовуйте [FromBody] для передачі даних в тілі запиту
        {
            // Map DTO to the CQRS command for updating the user
            var command = mapper.Map<UpdateUserCommand>(dto);
            command .UserId= (int)UserId; // Додайте ідентифікатор користувача для редагування
            await Mediator.Send(command);

            return NoContent(); // Поверніть 204 No Content після успішного редагування
        }


    }
}
