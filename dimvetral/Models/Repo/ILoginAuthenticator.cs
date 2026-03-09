namespace dimvetral.Models.Repo
{
    public interface ILoginAuthenticator
    {
        bool Authenticate(string userId, string password);
    }
}