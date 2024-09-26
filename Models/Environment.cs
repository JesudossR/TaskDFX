using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class Environment
    {
        [Key]
        [Column("ENVIRONMENTID")]
        public int EnvironmentID { get; set; } // Primary Key

        [Required(ErrorMessage = "Environment Name is required.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "EnvironmentName must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(20, ErrorMessage = "EnvironmentName should not contain more than 20 characters.")]
        [Column("ENVIRONMENTNAME")]
        public string EnvironmentName { get; set; } = string.Empty; // Required

        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "EnvironmentDescription must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(50, ErrorMessage = "EnvironmentDescription should not contain more than 50 characters.")]
        [Column("ENVIRONMENTDESCRIPTION")]
        public string? EnvironmentDescription { get; set; } // Optional
    }
}
