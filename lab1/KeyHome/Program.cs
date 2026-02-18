using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

using KeyHome.Forms; 
using JsonHandler;
using CryptoHandler;
using HashUtils;


namespace KeyHome
{
    internal static class Program
    {
        // для дебага
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        
        [STAThread]
        static void Main()
        {
            AllocConsole();
            // тестовый вывод
            // var fileHandler = CredentialJsonFileHandler.getInstance("Data/credentials.json");
            // var jsonHandler = new CredentialJsonHandler(fileHandler);
            // var fileHandler = new CredentialJsonHandler();
            // var file = fileHandler.File;
            // var textFile = file.FileReader.FileRead();
            
            //создание ключа
            string codeword = "рука";
            HashSHA256 hashSHA256 = new HashSHA256();
            byte[] key = hashSHA256.CreateHash(codeword);

            // шифрование
            // CryptAES256 cryptAES256 = new CryptAES256();
            // cryptAES256.encryption(textFile, key);
            // TestOpenSSL();


            ApplicationConfiguration.Initialize();
            
            Application.Run(new DataForm()); 
        }

        [DllImport("libcrypto-3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SHA256(
            byte[] data,
            UIntPtr len,
            byte[] md
        );
        static void TestOpenSSL()
        {
            string input = "hello openssl";
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] hash = new byte[32]; // SHA256 = 32 байта

            SHA256(data, (UIntPtr)data.Length, hash);

            Console.WriteLine("OpenSSL SHA256:");
            Console.WriteLine(BitConverter.ToString(hash).Replace("-", ""));
        }

    }
}
