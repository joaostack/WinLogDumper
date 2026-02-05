using Spectre.Console;

namespace WinLogDumper
{
    public class ConsoleUi
    {
        public static void Start()
        {
            while (true)
            {
                // SHOW ASCII ART
                Console.Clear();
                AnsiConsole.Write(
                    new FigletText("WinLogDumper")
                        .LeftJustified()
                        .Color(Color.Green));

                // MAIN MENU 
                var options = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose the option you want to dump")
                    .MoreChoicesText("[grey](Move up and down)[/]")
                    .AddChoices(new[] {
                    "Security/Auth Logs", "System Logs", "Application Logs",
                    "Hardware Logs", "Setup Logs", "Quit",
                }));

                switch (options)
                {
                    case "Security/Auth Logs":
                        Dumper.DumpSecLogs();
                        break;
                    case "Application Logs":
                        Dumper.DumpApplicationLogs();
                        break;
                    case "System Logs":
                        Dumper.DumpSystemLogs();
                        break;
                    case "Hardware Logs":
                        Dumper.DumpHardwareLogs();
                        break;
                    case "Setup Logs":
                        Dumper.DumpSetupLogs();
                        break;
                    case "Quit":
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}