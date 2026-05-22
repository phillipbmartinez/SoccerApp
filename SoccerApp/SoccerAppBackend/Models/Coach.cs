namespace SoccerAppBackend.Models
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string CoachingLicense { get; set; }
        public DateOnly StartedCoachingDate { get; set; }
        public int IsActive { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
