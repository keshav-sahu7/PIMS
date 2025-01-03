using PIMS.Models;

namespace PIMS.Services.UserServices;

public class UserInfoOutput
{
    public UserInfoOutput(User user)
    {
        UserId = user.UserId;
        UserName = user.Username;
        Role = user.Role;
    }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }

}