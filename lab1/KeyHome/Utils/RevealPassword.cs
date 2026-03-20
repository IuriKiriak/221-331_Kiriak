using System.Text;
using System.Text.Json;
using CryptoHandler;
using HashUtils;
using JsonHandler;
using KeyHome.Models;

namespace KeyHome;

public static class RevealPassword
{
    public static void RevealPassword1(string codeWord)
    {
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
public static class ProtectCredentials
{
    public static void EncryptCredentialsInsideFile(string codeWord)
    {
        var fileHandler = new CredentialJsonHandler();
        var fileCredential = fileHandler.File;

        string encryptedText = fileCredential.FileReader.FileRead();

        byte[] decryptedBytes = CryptAES256OpenSSL.Decrypt(encryptedText, codeWord);
        string decryptedJson = Encoding.UTF8.GetString(decryptedBytes);

        var credentialList = JsonSerializer.Deserialize<CredentialList>(decryptedJson);

        if (credentialList == null)
        {
            Console.WriteLine("Ошибка десериализации");
            return;
        }

        foreach (var cred in credentialList.Credentials)
        {
            byte[] loginEncrypted = CryptAES256OpenSSL.Encrypt(cred.Login, codeWord);
            byte[] passEncrypted = CryptAES256OpenSSL.Encrypt(cred.Password, codeWord);

            cred.Login = Convert.ToBase64String(loginEncrypted);
            cred.Password = Convert.ToBase64String(passEncrypted);
        }

        string updatedJson = JsonSerializer.Serialize(credentialList);

        byte[] finalEncrypted = CryptAES256OpenSSL.Encrypt(updatedJson, codeWord);
        string finalBase64 = Convert.ToBase64String(finalEncrypted);

        fileCredential.FileWritter.FileWritte(finalBase64);

        Console.WriteLine("Данные успешно перешифрованы");
    }
}


