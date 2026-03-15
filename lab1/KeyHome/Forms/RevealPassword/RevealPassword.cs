using System.Text;
using CryptoHandler;
using HashUtils;
using JsonHandler;

namespace KeyHome;

public static class RevealPassword
{
    public static void RevealPassword1()
    {
        string codeWord = "рука";

        var fileHandler = new CredentialJsonHandler();
        var fileCredential = fileHandler.File;
        string text = fileCredential.FileReader.FileRead();
        Console.WriteLine($"Текст из файла: {text}");
        Console.WriteLine("\n\n");

        byte[] originalBytes = Encoding.UTF8.GetBytes(text);
        byte[] key = HashSHA256.CreateHash(codeWord);


        byte[] iv = new byte[16]; 

        byte[] encrypted = CryptAES256OpenSSL.Encrypt(originalBytes, key, out iv);

        string encryptedBase64 = Convert.ToBase64String(encrypted);

        fileCredential.FileWritter.FileWritte(encryptedBase64);
        Console.WriteLine("Зашифрованные данные записаны в файл.");
    }

}