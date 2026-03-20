using System;
using System.Threading;
using System.Windows.Forms;

public static class AntiDebug
{
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool IsDebuggerPresent();

    public static void CheckDebugger()
    {
        if (IsDebuggerPresent())
        {
            // Можно вывести предупреждение пользователю
            MessageBox.Show(
                "Обнаружен отладчик! Программа будет закрыта.",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            // Завершение всего процесса
            Environment.Exit(1); // программа полностью завершится
        }
    }
}