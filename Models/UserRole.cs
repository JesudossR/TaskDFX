using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class UserRole
    {
        [Key]
        [Column("USER_ID")]

        public int UserID { get; set; } // Primary Key

        [Required(ErrorMessage = "Role is required.")]
        [Column("ROLE")]
        // [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "Role must start with a letter and contain only letters, numbers, and spaces.")]
        // [StringLength(10, ErrorMessage = "Role should not contain more than 10 characters.")]
        public string Role { get; set; } = string.Empty; // Required

        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "RoleDescription must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(10, ErrorMessage = "RoleDescription should not contain more than 10 characters.")]
        [Column("ROLE_DESCRIPTION")]
        public string? RoleDescription { get; set; } // Optional
    }
}
