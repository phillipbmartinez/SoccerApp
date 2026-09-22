namespace SoccerAppBackend.Models
{
    public class GameTeamOpponentDto
    {
        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public string? GameLocation { get; set; }
        public string? GameStatus { get; set; }
        public string? AgeGroup { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? TeamScore { get; set; }
        public int OpponentId { get; set; }
        public string OpponentName { get; set; }
        public int? OpponentScore { get; set; }
    }
}
