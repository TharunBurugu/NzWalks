namespace NZWalksAPI.Models.Domain
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Type { get; set; }    
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }

        public Guid RegionId { get; set; }



        // Navigation property
        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }
    }
}
