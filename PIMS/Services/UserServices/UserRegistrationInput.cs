using PIMS.Models;

namespace PIMS.Services.UserServices;

public class UserRegistrationInput
{
    public User ToUserEntity()
    {
        User user = new User()
        {
            UserId = Guid.NewGuid().ToString(),
            Username = UserName,
            Role = Role,
            PasswordHash = Utility.Utility.ComputeSha256Hash(Password)
        };
        return user;
    }

    public string UserName { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
}