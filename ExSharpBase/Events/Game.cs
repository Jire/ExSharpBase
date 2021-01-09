using System;
using System.Management;
using ExSharpBase.Modules;

namespace ExSharpBase.Events
{
    internal static class Game
    {
        public static void OnGameLoad(object sender, EventArrivedEventArgs e)
        {
            if (e.NewEvent.Properties["ProcessName"].Value.ToString().Equals("League of Legends.exe"))
            {
                //On Game Load event
                LogService.Log("League Started");
            }
        }

        public static void OnGameExit(object sender, EventArrivedEventArgs e)
        {
            if (e.NewEvent.Properties["ProcessName"].Value.ToString().Equals("League of Lege"))
            {
                Environment.Exit(0);
            }
        }
    }
}