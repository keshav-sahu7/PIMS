using PIMS.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIMS.Repository;
using PIMS.Services.UserServices;
using PIMS.Utility;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _users;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _users = _unitOfWork.GetRepository<User>();
    }

    public async Task<UserInfoOutput> RegisterUser(UserRegistrationInput registrationInput)
    {
        User user = registrationInput.ToUserEntity();
        _users.Add(user);
        await _unitOfWork.SaveChangesAsync();
        return new UserInfoOutput(user);
    }

    public async Task<UserLoginOutput> Login(UserLoginInput loginInput)
    {
        var user = await (from usr in _users.GetAll()
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
        var user = await _users.GetAll().FirstOrDefaultAsync(user => user.UserId == userId);
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