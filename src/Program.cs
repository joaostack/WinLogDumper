using System.Security.Principal;

namespace WinLogDumper
{
    class Program
    {
        static void Main()
        {
            // Check if target OS is Linux
            if (System.OperatingSystem.IsLinux() || System.OperatingSystem.IsMacOS())
            {
                Console.WriteLine("This program is only for Windows OS");
                return;
            }

            // Check admin privileges
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("Please run this program as an administrator");
                return;
            }

            ConsoleUi.Start();
        }
    }
}