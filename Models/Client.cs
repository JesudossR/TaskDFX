using System.ComponentModel.DataAnnotations;

namespace DolphinFx.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; } // Primary Key

        [Required]
        public string ClientName { get; set; } = string.Empty; // Required and non-nullable

        [Required(ErrorMessage = "Primary Contact is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Primary Contact must be exactly 10 digits.")]
        [MaxLength(10)]
        public long PrimaryContact { get; set; } // 10 digits

        [Required]
        [EmailAddress]
        public string PrimaryEmailID { get; set; } = string.Empty; // Required and valid email

        // [Range(1000000000, 9999999999)] // Optional but if provided, must be 10 digits
        public long? SecondaryContact { get; set; } // 10 digits

        [EmailAddress]
        public string? SecondaryEmailID { get; set; } // Optional and valid email

        // Navigation property
        public virtual ICollection<Team>? Teams { get; set; } // One client can have many teams
        public virtual ICollection<ApplicationDetails>? ApplicationDetails { get; set; } // Navigation property for ApplicationDetails
    }
}
