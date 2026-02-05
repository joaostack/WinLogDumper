using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Spectre.Console;

namespace WinLogDumper
{
    public class Dumper
    {
        // Get current date and time
        protected static DateTime now = DateTime.Now;
        protected static string formattedDate = now.ToString("dd-MM-yyyy-HH-mm-ss");

        // Logs path
        protected static string secLogs = @"C:\Windows\System32\winevt\Logs\Security.evtx";
        protected static string appLogs = @"C:\Windows\System32\winevt\Logs\Application.evtx";
        protected static string hardwareLogs = @"C:\Windows\System32\winevt\Logs\HardwareEvents.evtx";
        protected static string systemLogs = @"C:\Windows\System32\winevt\Logs\System.evtx";
        protected static string setupLogs = @"C:\Windows\System32\winevt\Logs\Setup.evtx";

        public static void EventReaderWriter(string output, string path)
        {
            using (var reader = new EventLogReader(path, PathType.FilePath))
            {
                EventRecord record;
                using (var sw = new StreamWriter(output))
                {
                    while ((record = reader.ReadEvent()) != null)
                    {
                        using (record)
                        {
                            sw.WriteLine("{0} {1}: {2}", record.TimeCreated?.ToString() ?? "null", record.LevelDisplayName ?? "null", record.FormatDescription() ?? "null");
                        }
                    }
                }
            }
        }

        public static void DumpLogs(string logType, string formattedDate, Action<string> logAction)
        {
            string fileName = $"dump_{logType}-{formattedDate}.txt";
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .Start($"Dumping {logType} logs...", ctx =>
                {
                    logAction(fileName);
                });

            AnsiConsole.Markup($"[green]{logType} logs dumped![/]\n");
            AnsiConsole.Markup($"[yellow]{fileName}[/]\n");
            AnsiConsole.Markup("Press any key to continue...\n");
            Console.ReadKey();
        }

        public static void DumpApplicationLogs()
        {
            DumpLogs("application", formattedDate, logAction =>
            {
                EventReaderWriter(logAction, appLogs);
            });
        }

        public static void DumpHardwareLogs()
        {
            DumpLogs("hardware", formattedDate, logAction =>
            {
                EventReaderWriter(logAction, hardwareLogs);
            });
        }

        public static void DumpSystemLogs()
        {
            DumpLogs("system", formattedDate, logAction =>
            {
                EventReaderWriter(logAction, systemLogs);
            });
        }

        public static void DumpSetupLogs()
        {
            DumpLogs("setup", formattedDate, logAction =>
            {
                EventReaderWriter(logAction, setupLogs);
            });
        }

        public static void DumpSecLogs()
        {
            DumpLogs("security", formattedDate, logAction =>
            {
                EventReaderWriter(logAction, secLogs);
            });
        }
    }
}