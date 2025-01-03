using PIMS.Models;

public class UserLoginOutput
{
    public UserLoginOutput(User user, string accessToken)
    {
        AccessToken = accessToken;
    }
    
    public string AccessToken { get; set; }
}