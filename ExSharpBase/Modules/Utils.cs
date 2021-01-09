using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ExSharpBase.Modules
{
    internal static class Utils
    {
        public static Process GetLeagueProcess()
        {
            try
            {
                return Process.GetProcessesByName("League of Legends").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Could Not Find League of Legend's Process {ex}");
                return null;
            }
        }

        public static bool IsGameOnDisplay()
        {
            //TODO: HWND Locking up on League's HWND
            return NativeImport.GetActiveWindowTitle() == "League of Legends (TM) Client";
        }

        public static bool IsKeyPressed(Keys keys)
        {
            return 0 != (NativeImport.GetAsyncKeyState((int) keys) & 0x8000);
        }

        public static bool IsKeyPressed(uint keys)
        {
            return 0 != (NativeImport.GetAsyncKeyState((int) keys) & 0x8000);
        }
    }
}