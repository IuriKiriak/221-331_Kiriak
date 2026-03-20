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
        AntiDebug.CheckDebugger();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ApplicationConfiguration.Initialize();
        // RevealPassword.RevealPassword1("пароль");
        // ProtectCredentials.EncryptCredentialsInsideFile("пароль");

        Application.Run(new LoginForm());
    }
}

