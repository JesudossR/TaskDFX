using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class Application
    {
        [Key]
        [Column("APPLICATION_ID")]
        public int ApplicationID { get; set; } // Primary Key
        [Column("APPLICATION_NAME")]
        [Required(ErrorMessage = "Application Name is required.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "ApplicationName must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(30, ErrorMessage = "ApplicationName should not contain more than 20 characters")]
        public string ApplicationName { get; set; } = string.Empty; // Required
        [Column("APPLICATION_SHORTNAME")]
        [Required(ErrorMessage = "ApplicationShortName is required.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "ApplicationShortName must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(10, ErrorMessage = "ApplicationName should not contain more than 5 characters")]
        public string ApplicationShortName { get; set; } = string.Empty; // Required

        [Column("APPLICATION_DESCRIPTION")]
        [StringLength(50, ErrorMessage = "ApplicationDescription should not contain more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "ApplicationDescription must start with a letter and contain only letters, numbers, and spaces.")]
        public string? ApplicationDescription { get; set; } // Optional
        public virtual ICollection<ApplicationDetails>? ApplicationDetails { get; set; } // Navigation property for ApplicationDetails
    }
}
