using System.ComponentModel.DataAnnotations;

namespace car1.Models
{
    public class Login
    {
        [Key]
        [StringLength(255)] // Limit the length of the username to 255 characters
        public string Username { get; set; }

        public string Password { get; set; }

        // Foreign Key to UserType
        public int UserTypeId { get; set; }  // Add this line to create the foreign key relationship

        // Navigation property
        public virtual UserType UserType { get; set; }
    }
}
