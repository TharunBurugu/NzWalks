using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum 3 charecters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum 3 charecters")]
        public string Code { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum 3 charecters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum 3 charecters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
