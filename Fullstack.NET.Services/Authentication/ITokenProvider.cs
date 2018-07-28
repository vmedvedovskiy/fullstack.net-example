namespace Fullstack.NET.Services.Authentication
{
    public interface ITokenProvider
    {
        bool IsValid(string token);

        string Get(UserModel user);
    }
}
