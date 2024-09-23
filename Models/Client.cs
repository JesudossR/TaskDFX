using System.ComponentModel.DataAnnotations;


namespace DolphinFx.Models
{
    public class Client

    {
      
        [Key]
        public int ClientID { get; set; } // Primary Key

        [Required(ErrorMessage = "Client Name is required.")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9\\s]*$", ErrorMessage = "Client Name must start with a letter and can contain letters, numbers, and spaces.")]
        [StringLength(20, ErrorMessage = "Client Name cannot exceed 20 characters.")]
        public string ClientName { get; set; } = string.Empty; // Required and non-nullable

       
         [Required(ErrorMessage = "Primary Contact is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Primary Contact must be a 10-digit number.")]
        public long PrimaryContact { get; set; } // 10 digits

        [Required(ErrorMessage = "Primary Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format for Primary Email.")]
         
        public string PrimaryEmailID { get; set; } = string.Empty; // Required and valid email

      
         
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Primary Contact must be a 10-digit number.")]
        public long? SecondaryContact { get; set; } // 10 digits

        public string? SecondaryEmailID { get; set; } // Optional and valid email

        // Navigation property
        public virtual ICollection<Team>? Teams { get; set; } // One client can have many teams -- This is represented by the Teams collection in the Client class.
        //establishing relationship -> 1 - to - many
        /*
                          public virtual ICollection<Team>? Teams { get; set; } 

                                                                      && 

                          modelBuilder.Entity<Client>()
                         .HasMany(c => c.Teams)
                         .WithOne(t => t.Client)
                         .HasForeignKey(t => t.ClientID);
                                                                      --> same meaning 
         */
        public virtual ICollection<ApplicationDetails>? ApplicationDetails { get; set; } // Navigation property for ApplicationDetails
    }
}
