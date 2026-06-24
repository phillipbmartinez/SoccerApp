namespace SoccerAppBackend.Models
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }
        public int? UserId { get; set; }
        public int? TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? JerseyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
