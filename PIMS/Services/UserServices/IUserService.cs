namespace PIMS.Services.UserServices;

public interface IUserService
{
    Task<UserInfoOutput> RegisterUser(UserRegistrationInput registrationInput);
    Task<UserLoginOutput> Login(UserLoginInput loginInput);
    Task<UserInfoOutput> GetUserById(string userId);
}