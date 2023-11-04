using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto;
using NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl;
using NexTube.WebApi.DTO.Files.Photo;
using WebShop.Domain.Constants;

namespace NexTube.WebApi.Controllers
{
    public class PhotoController : BaseController
    {
        private readonly IMapper mapper;

        public PhotoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        
        [HttpGet("{photoId}")]
        public async Task<ActionResult> GetPhotoUrl(string photoId)
        {
            var getPhotoUrlDto = new GetPhotoUrlDto()
            {
                PhotoId = photoId,
            };

            var query = mapper.Map<GetPhotoUrlQuery>(getPhotoUrlDto);
            var getPhotoUrlVm = await Mediator.Send(query);

            return Redirect(getPhotoUrlVm.PhotoUrl);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        public async Task<ActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
        {
            var command = mapper.Map<UploadPhotoCommand>(dto);
            var photoId = await Mediator.Send(command);

            return Ok(photoId);
        }
    }
}
