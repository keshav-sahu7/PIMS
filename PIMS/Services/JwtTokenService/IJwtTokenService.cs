namespace PIMS.Services.JwtTokenService;

public interface IJwtTokenService
{
    string GenerateJwtToken(string userId, string role);
}