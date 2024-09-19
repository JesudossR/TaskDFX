using System.ComponentModel.DataAnnotations;

namespace DolphinFx.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; } // Primary Key

        [Required(ErrorMessage = "Application Name is required.")]
         [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "ApplicationName must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(20, ErrorMessage = "ApplicationName should not contain more than 20 characters")]
        public string ApplicationName { get; set; } = string.Empty; // Required

        [Required(ErrorMessage = "ApplicationShortName is required.")]
         [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "ApplicationShortName must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(5, ErrorMessage = "ApplicationName should not contain more than 5 characters")]
        public string ApplicationShortName { get; set; } = string.Empty; // Required
        [StringLength(50, ErrorMessage = "ApplicationDescription should not contain more than 50 characters")]
         [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "ApplicationDescription must start with a letter and contain only letters, numbers, and spaces.")]
        public string? ApplicationDescription { get; set; } // Optional
        public virtual ICollection<ApplicationDetails>? ApplicationDetails { get; set; } // Navigation property for ApplicationDetails
    }
}
