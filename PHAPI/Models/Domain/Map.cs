namespace PHAPI.Models.Domain
{
    public class Map
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? MapUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid LGAId { get; set; }

        // Relationship of navigation properties
        public Difficulty Difficulty { get; set; }

        public LGA  LGA { get; set; }


    }
}
