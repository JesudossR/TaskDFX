using System.ComponentModel.DataAnnotations;

namespace DolphinFx.Models
{
    public class UserRole
    {
        [Key]
        public int UserID { get; set; } // Primary Key

        [Required]
        public string Role { get; set; } = string.Empty; // Required

        public string? RoleDescription { get; set; } // Optional
    }
}
