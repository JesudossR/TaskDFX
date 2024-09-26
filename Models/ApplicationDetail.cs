using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DolphinFx.Models
{
  public class ApplicationDetails
  {
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Client")]
    [Column("CLIENT_ID")]
    public int ClientID { get; set; } // Foreign Key to Client
    public virtual Client? Client { get; set; } // Navigation property
    [Required]
    [Column("ENVIRONMENT_ID")]
    [ForeignKey("Environment")]
    public int EnvironmentID { get; set; }
    public virtual Environment? Environments { get; set; } // Navigation property
    [Required]
    [ForeignKey("Application")]
    [Column("APPLICATION_ID")]
    public int ApplicationID { get; set; } // Foreign Key to Application
    public virtual Application? Applications { get; set; } // Navigation property

    [Required]
    [Url(ErrorMessage = "Invalid URL format.")]
    [Column("LINK")]
    public string Link { get; set; } = string.Empty; // Required

    [Required(ErrorMessage = "Please enter a path")]
    [Column("PATH")]
    public string Path { get; set; } = string.Empty; // Required


    [Required(ErrorMessage = "User Name is required.")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "UserName must start with a letter and contain only letters, numbers, and spaces.")]
    [StringLength(20, ErrorMessage = "UserName should not contain more than 20 characters.")]
    [Column("USER_NAME")]
    public string? User { get; set; }

    [Required]
    [ForeignKey("UserRole")]
    [Column("USER_ID")]

    public int UserId { get; set; } // Foreign Key to Application
    public virtual UserRole? UserRole { get; set; } // 


    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!#$%^&*]).{8,}$",
             ErrorMessage = "Password must be atleast 8 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    [Column("PASSWORD")]
    public string Password { get; set; } = string.Empty;

    //[Required]
    //public string ClientName { get; set; } = string.Empty; // Required
    //
    //[Required]
    //public string Environment { get; set; } = string.Empty; // Required

    //[Required]
    //public string ApplicationName { get; set; } = string.Empty; // Required

    //public virtual ICollection<Application>? Application { get; set; } // Navigation property for ApplicationDetails
    //[Required]
    //public string? UserRole { get; set; }
  }
}
