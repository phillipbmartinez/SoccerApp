using System.ComponentModel.DataAnnotations;

namespace ArsenalAcademy.Models
{
    public class CoachViewModel
    {
        public int CoachId { get; set; }
        public string? CoachingLicense { get; set; }
        public DateTime StartedCoachingDate { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
    }
}
