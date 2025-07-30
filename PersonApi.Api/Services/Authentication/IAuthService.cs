namespace PersonApi.Api.Services.Authentication;

public interface IAuthService
{
    string GenerateJwtToken(string userId);
}