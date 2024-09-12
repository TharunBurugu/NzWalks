namespace NZWalksAPI.Models.DTO
{
    public class WalkDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Type { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }

        public Guid RegionId { get; set; }


        public RegionDto Region { get; set; }
        public DiffucultyDto Diffuculty { get; set; }
    }
}
