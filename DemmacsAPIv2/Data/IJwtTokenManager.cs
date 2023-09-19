namespace DemmacsAPIv2.Data
{
    public interface IJwtTokenManager
    {
        string Authenticate(string email, string password);
    }
}
