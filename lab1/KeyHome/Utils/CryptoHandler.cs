using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using HashUtils;

namespace CryptoHandler;

interface ICryptAES256
{
    void encryption(string text, byte[] key);
    void decryption(string text, byte[] key);
}

public class CryptAES256 : ICryptAES256
{
    public void encryption(string text, byte[] key)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.Key = key;
        aes.GenerateIV();
        byte[] iv = aes.IV;
        byte[] encryptedText;

        using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
        using (var ms = new MemoryStream())
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(text);
            sw.Flush();
            cs.FlushFinalBlock();
            encryptedText = ms.ToArray();
        }

        Console.WriteLine("Зашифрованный текст (Base64): " + Convert.ToBase64String(encryptedText));
        
        Console.WriteLine(encryptedText.GetType());
    }
    public void decryption(string text, byte[] key)
    {
        
    }
}