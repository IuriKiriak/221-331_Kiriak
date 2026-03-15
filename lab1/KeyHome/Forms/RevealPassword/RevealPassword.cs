using System.Text;
using CryptoHandler;
using HashUtils;
using JsonHandler;

namespace KeyHome;

public static class RevealPassword
{
    public static void RevealPassword1()
    {
        // тут зашифрование файла так что не юзаем
        string codeWord = "рука";

        var fileHandler = new CredentialJsonHandler();
        var fileCredential = fileHandler.File;
        string text = fileCredential.FileReader.FileRead();
        
        Console.WriteLine($"Текст из файла: {text}");
        Console.WriteLine("\n\n");

        byte[] encrypted = CryptAES256OpenSSL.Encrypt(text, codeWord);

        string encryptedBase64 = Convert.ToBase64String(encrypted);

        fileCredential.FileWritter.FileWritte(encryptedBase64);
        Console.WriteLine("Зашифрованные данные записаны в файл.");
    }

}