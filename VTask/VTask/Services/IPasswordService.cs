namespace VTask.Services
{
    public interface IPasswordService
    {
        (byte[] passwordHash, byte[] PasswordSalt) CreatePasswordHashSalt(string password);
        bool IsValidPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
