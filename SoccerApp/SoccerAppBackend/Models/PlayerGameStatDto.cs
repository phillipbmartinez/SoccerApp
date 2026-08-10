namespace SoccerAppBackend.Models
{
    public class PlayerGameStatDto
    {
        public int PlayerGameStatId { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int? MinutesPlayed { get; set; }
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public int? Shots { get; set; }
        public int? ShotsOnTarget { get; set; }
        public int? PassesCompleted { get; set; }
        public int? Tackles { get; set; }
        public int? Interceptions { get; set; }
        public int? Saves { get; set; }
        public int? YellowCards { get; set; }
        public int? RedCards { get; set; }
        public string? Notes { get; set; }
    }
}
