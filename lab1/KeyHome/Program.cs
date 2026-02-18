using System;
using System.Windows.Forms;
using KeyHome.Forms; 
using JsonHandler;
using FileHandler;

using System.Runtime.InteropServices;
using System.Windows.Forms;

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
            var fileHandler = new CredentialJsonHandler();
            var file = fileHandler.File;
            var textFile = file.FileReader.FileRead();
            Console.WriteLine(textFile);
            var testText= "sadasd";
            file.FileWritter.FileWritte(testText);

            ApplicationConfiguration.Initialize();
            
            Application.Run(new DataForm()); 
        }
    }
}
