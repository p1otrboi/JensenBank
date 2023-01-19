namespace JensenBank.Repository.Authentication;

public interface IPasswordEncryption
{
    public string HashPassword(string password, out byte[] salt);
    public bool VerifyPassword(string password, string hash, byte[] salt);
}
