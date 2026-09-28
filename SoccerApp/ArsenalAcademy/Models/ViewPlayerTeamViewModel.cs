namespace ArsenalAcademy.Models
{
    public class ViewPlayerTeamViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int? JerseyNumber { get; set; }
        public bool PlayerIsActive { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string? AgeGroup { get; set; }
        public bool TeamIsActive { get; set; }
    }
}
