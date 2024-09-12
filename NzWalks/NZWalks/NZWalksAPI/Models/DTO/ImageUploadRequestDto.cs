using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileNmae { get; set; }
        [Required]
        public string? FileDescriptoin { get; set; }

    }
}
