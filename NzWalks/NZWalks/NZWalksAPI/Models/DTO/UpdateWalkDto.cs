using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateWalkDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Name hase to be maxmium 10 charecters")]
        [MinLength(3, ErrorMessage = "Name hase to be Minimum 3 charecters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name hase to be maxmium 100 charecters")]
        [MinLength(10, ErrorMessage = "Name hase to be Minimum 10 charecters")]
        public string Description { get; set; }
        public double Type { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyID { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
