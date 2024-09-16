using System.ComponentModel.DataAnnotations;

namespace DolphinFx.Models
{
    public class Environment
    {
        [Key]
        public int EnvironmentID { get; set; } // Primary Key

        [Required]
        public string EnvironmentName { get; set; } = string.Empty; // Required

        public string? EnvironmentDescription { get; set; } // Optional
    }
}
