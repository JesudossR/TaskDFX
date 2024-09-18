using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class DatabaseDetail
    {
        [Key]
        public int DbId { get; set; }
        [Required]
        public string? ClientName { get; set; }
        [Required]
        public string? Environment { get; set; }
        [Required]
        public string? ApplicationName { get; set; }
        [Required]
        public string? Datasource { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        [ForeignKey("Environment")]
        public int EnvironmentID { get; set; }
        public virtual Environment? Environments { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }
        public virtual Application? Application { get; set; }


    }
}
