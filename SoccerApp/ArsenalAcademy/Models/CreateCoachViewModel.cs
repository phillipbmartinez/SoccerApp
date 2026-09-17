namespace ArsenalAcademy.Models
{
    public class CreateCoachViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? CoachingLicense { get; set; }
        public DateTime StartedCoachingDate { get; set; }
        public string? TeamName { get; set; }
    }
}
