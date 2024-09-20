using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
    public class DatabaseDetail
    {
        [Key]
        public int DbId { get; set; }
        //[Required]
        //public string? ClientName { get; set; }
        //[Required]
        //public string? Environment { get; set; }
        //[Required]
        //public string? ApplicationName { get; set; }
        [Required]
        public string? Datasource { get; set; }
       [Required(ErrorMessage = "User Name is required.")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "UserName must start with a letter and contain only letters, numbers, and spaces.")]
    [StringLength(20, ErrorMessage = "UserName should not contain more than 20 characters.")]
        public string? Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!#$%^&*]).{8,}$",
            ErrorMessage = "Password must be atleast 8 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]

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
