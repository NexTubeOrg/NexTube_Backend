using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto;
using NexTube.Application.CQRS.Files.Photos.Queries.GetPhoto;
using NexTube.WebApi.DTO.Files.Photo;

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
        public async Task<ActionResult> GetPhoto(string photoId)
        {
            var getPhotoDto = new GetPhotoDto()
            {
                PhotoId = photoId
            };

            var query = mapper.Map<GetPhotoQuery>(getPhotoDto);
            var getPhotoVm = await Mediator.Send(query);

            return File(getPhotoVm.PhotoStream, "image/png");
        }

        [HttpPost]
        public async Task<ActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
        {
            var command = mapper.Map<UploadPhotoCommand>(dto);
            var photoId = await Mediator.Send(command);

            return Ok(photoId);
        }
    }
}
