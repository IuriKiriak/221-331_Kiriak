using System;
using System.Windows.Forms;
using KeyHome.Forms; 

namespace KeyHome
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new DataForm()); 
        }
    }
}
