namespace SoccerAppBackend.Models
{
    public class GameDto
    {
        public int GameId { get; set; }
        public int TeamId { get; set; }
        public int OpponentId { get; set; }
        public DateTime GameDate { get; set; }
        public string? GameLocation { get; set; }
        public int? TeamScore { get; set; }
        public int? OpponentScore { get; set; }
        public string? Notes { get; set; }
    }
}
