namespace SoccerAppBackend.Models
{
    public class TeamCoachDto
    {
        public int CoachId { get; set; }
        public int TeamId { get; set; }
        public int? UserId { get; set; }
        public string TeamName { get; set; }
        public string? AgeGroup { get; set; }
        public string CoachFirstName { get; set; }
        public string CoachLastName { get; set; }
    }
}
