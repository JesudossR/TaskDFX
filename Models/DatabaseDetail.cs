using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class DatabaseDetail
    {
        [Key]
        [Column("DB_ID")]
        public int DbId { get; set; }

        [Required]
        [Column("DATASOURCE")]
        public string? Datasource { get; set; }
        [Required(ErrorMessage = "User Name is required.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "UserName must start with a letter and contain only letters, numbers, and spaces.")]
        [StringLength(20, ErrorMessage = "UserName should not contain more than 20 characters.")]
        [Column("USERNAME")]
        public string? Username { get; set; }
        [Required]
        
        [Column("PASSWORD")]

        public string? Password { get; set; }

        [ForeignKey("Client")]
        [Column("CLIENT_ID")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        [ForeignKey("Environment")]
        [Column("ENVIRONMENT_ID")]
        public int EnvironmentID { get; set; }
        public virtual Environment? Environments { get; set; }

        [ForeignKey("Application")]
        [Column("APPLICATION_ID")]
        public int ApplicationID { get; set; }
        public virtual Application? Application { get; set; }


    }
}
