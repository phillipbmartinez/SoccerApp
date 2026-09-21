using System.ComponentModel.DataAnnotations;

namespace ArsenalAcademy.Models
{
    public class CreateCoachViewModel
    {
        public int CoachId { get; set; }
        public int? UserId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        public string? CoachingLicense { get; set; }

        [Required(ErrorMessage = "Coaching date required is required.")]
        public DateTime StartedCoachingDate { get; set; }

        public string? TeamName { get; set; }
    }
}
