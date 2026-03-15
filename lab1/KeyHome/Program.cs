using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using KeyHome.Forms;
using JsonHandler;
using CryptoHandler;
using HashUtils;
using FileHandler;


namespace KeyHome;

internal static class Program
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [STAThread]
    static void Main()
    {
        AllocConsole();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ApplicationConfiguration.Initialize();

        var fileHandler = new CredentialJsonHandler();
        var file = fileHandler.File;


        string text = file.FileReader.FileRead();
        Console.WriteLine($"{text}\n\n\n");
        // TestCript.TestCript1();

        Application.Run(new Form1());
    }
}

