using System.ComponentModel.DataAnnotations;

namespace PIMS.Models;
public class User
{
    [Key]
    public string UserId { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required]
    public byte[] PasswordHash { get; set; }
    
    [Required, MaxLength(20)]
    public string Role { get; set; } // Admin, User
}
