using System.Security.Cryptography;
using System.Text;

namespace HashUtils;

public interface IHashSHA256
{
    byte[] CreateHash(string codeWord);
}

public class HashSHA256 : IHashSHA256
{
    public byte[] CreateHash(string codeWord) 
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(Encoding.UTF8.GetBytes(codeWord));
    }
}
