namespace PIMS.Services.UserServices;

public interface IUserService
{
    UserInfoOutput RegisterUser(UserRegistrationInput registrationDto);
    string Login(UserLoginInput loginDto);
    UserInfoOutput GetUserById(string userId);
}