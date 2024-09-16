using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; } // Primary Key
        [Required]
        public string? TeamName { get; set; } = string.Empty;
        [Required]
        public string? TeamDescription { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Client")]
        public int ClientID { get; set; } //Foreign Key to client

        public virtual Client? Client { get; set; } // Navigation property
    }
}
