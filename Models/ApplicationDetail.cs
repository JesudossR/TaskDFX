using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class ApplicationDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientID { get; set; } // Foreign Key to Client
        public virtual Client? Client { get; set; } // Navigation property

        [Required]
        [ForeignKey("Application")]
        public int ApplicationID { get; set; } // Foreign Key to Application
        public virtual Application? Applications { get; set; } // Navigation property
        [Required]
        public string ClientName { get; set; } = string.Empty; // Required

        [Required]
        public string Environment { get; set; } = string.Empty; // Required

        [Required]
        public string ApplicationName { get; set; } = string.Empty; // Required

        [Required]
        public string Link { get; set; } = string.Empty; // Required

        [Required]
        public string Path { get; set; } = string.Empty; // Required

        //public virtual ICollection<Application>? Application { get; set; } // Navigation property for ApplicationDetails
    }
}
