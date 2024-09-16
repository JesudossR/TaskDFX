using System.ComponentModel.DataAnnotations;

namespace DolphinFx.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; } // Primary Key

        [Required]
        public string ApplicationName { get; set; } = string.Empty; // Required

        [Required]
        public string ApplicationShortName { get; set; } = string.Empty; // Required

        public string? ApplicationDescription { get; set; } // Optional
        public virtual ICollection<ApplicationDetails>? ApplicationDetails { get; set; } // Navigation property for ApplicationDetails
    }
}
