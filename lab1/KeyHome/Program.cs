using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using KeyHome.Forms;
using JsonHandler;
using CryptoHandler;
using HashUtils;
using FileHandler;

namespace KeyHome
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole();

            try
            {
                var fileHandler = new CredentialJsonHandler();
                var file = fileHandler.File;


                string text = file.FileReader.FileRead();
                Console.WriteLine($"{text}\n\n\n");


                byte[] originalBytes = Encoding.UTF8.GetBytes(text);


                string codeWord = "рука";
                byte[] key = HashSHA256.CreateHash(codeWord); 
                Console.WriteLine($"Ключ (hex): {BitConverter.ToString(key)}");

                byte[] encrypted = CryptAES256OpenSSL.Encrypt(originalBytes, key, out byte[] iv);
                Console.WriteLine($"Encrypted (Base64): {Convert.ToBase64String(encrypted)}");
                Console.WriteLine($"IV (Base64): {Convert.ToBase64String(iv)}");

                byte[] decrypted = CryptAES256OpenSSL.Decrypt(encrypted, key, iv);
                Console.WriteLine($"Decrypted (hex): {BitConverter.ToString(decrypted)}");

                string decryptedText = Encoding.UTF8.GetString(decrypted);
                Console.WriteLine($"Decrypted text:\n{decryptedText}");

                file.FileWritter.FileWritte(decryptedText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new DataForm());
        }
    }
}