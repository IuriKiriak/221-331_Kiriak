using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class AntiDebug
{
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool IsDebuggerPresent();

    public static void CheckDebugger()
    {
        if (IsDebuggerPresent())
        {
            MessageBox.Show(
                "Обнаружен отладчик! Программа будет закрыта.",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );


            Environment.Exit(1);
        }
    }
}