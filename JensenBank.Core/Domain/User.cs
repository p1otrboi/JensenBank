namespace JensenBank.Core.Domain;

public class User
{
    public int UserId { get; set; }
    public int CustomerId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PW_Hash { get; set; } = string.Empty;
    public string PW_Salt { get; set; } = string.Empty;
    public string Role_Type { get; set; } = string.Empty;
}
