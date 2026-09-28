namespace SoccerAppBackend.Models
{
    public class PlayerTeamDto
    {
        public int PlayerId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int? JerseyNumber { get; set; }
        public int PlayerIsActive { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string? AgeGroup { get; set; }
        public int TeamIsActive { get; set; }
    }
}
