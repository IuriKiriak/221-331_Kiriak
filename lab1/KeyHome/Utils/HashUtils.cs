using System.Security.Cryptography;
using System.Text;

namespace HashUtils;

public class HashSHA256
{
    public static byte[] CreateHash(string codeWord) 
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(Encoding.UTF8.GetBytes(codeWord));
        
    }
}
