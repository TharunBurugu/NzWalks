using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3 , ErrorMessage = "Code has to be minimum 3 charecters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum 3 charecters")]
        public string Code { get; set; }
        [MaxLength(100, ErrorMessage = "Code hase to be maximum 100 charecters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
