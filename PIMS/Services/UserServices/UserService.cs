using PIMS.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIMS.Services.UserServices;
using PIMS.Utility;

public class UserService : IUserService
{
    private readonly PimsContext _dbContext;

    public UserService(PimsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserInfoOutput> RegisterUser(UserRegistrationInput registrationInput)
    {
        User user = registrationInput.ToUserEntity();
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
        return new UserInfoOutput(user);
    }

    public async Task<UserLoginOutput> Login(UserLoginInput loginInput)
    {
        var user = await (from usr in _dbContext.Users
            where usr.Username == loginInput.Username
            select usr).FirstOrDefaultAsync();
        if (user == null || !VerifyPassword(user.PasswordHash, loginInput.Password))
        {
            throw new Exception("Invalid credentials.");
        }

        var accessToken = GenerateToken(user);
        return new UserLoginOutput(user, accessToken);
    }

    public async Task<UserInfoOutput> GetUserById(string userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        return new UserInfoOutput(user);
    }

    private bool VerifyPassword(byte[] password, string inputPassword)
    {
        return inputPassword.CompareHash(password);
    }

    private string GenerateToken(User user)
    {
        return "";
    }
}