namespace SoccerAppBackend.Models
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string? CoachingLicense { get; set; }
        public DateTime StartedCoachingDate { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
