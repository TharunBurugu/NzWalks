using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.IRepository;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageInterface imageRepository;

        public ImageController(IImageInterface imageInterface)
        {
            this.imageRepository = imageInterface;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> ImageUpload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                // Convert Dto to Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileDescription = request.FileDescriptoin,
                    FileExtention = Path.GetExtension(request.File.FileName).Trim(),
                    FileName = request.FileNmae,
                    FileSize = request.File.Length
                };
                // use repository to upload image
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] {".jpg",".jpeg",".png" };

            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Please upload valid size");
            }
        }
    }
}
