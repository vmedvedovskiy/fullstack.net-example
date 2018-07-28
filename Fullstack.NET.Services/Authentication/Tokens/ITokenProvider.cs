namespace Fullstack.NET.Services.Authentication.Tokens
{
    public interface ITokenProvider
    {
        bool IsValid(string token);

        string Get(UserModel user);
    }
}
