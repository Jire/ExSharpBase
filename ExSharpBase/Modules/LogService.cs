﻿using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ExSharpBase.Enums;
using Microsoft.Win32.SafeHandles;

namespace ExSharpBase.Modules
{
    class LogService
    {
        public static void CreateConsole()
        {
            NativeImport.AllocConsole();

            var outFile = NativeImport.CreateFile("CONOUT$", NativeImport.GENERIC_WRITE | NativeImport.GENERIC_READ,
                NativeImport.FILE_SHARE_WRITE, 0, NativeImport.OPEN_EXISTING, /*FILE_ATTRIBUTE_NORMAL*/0, 0);
            var safeHandle = new SafeFileHandle(outFile, true);

            NativeImport.SetStdHandle(-11, outFile);

            var fs = new FileStream(safeHandle, FileAccess.Write);
            var writer = new StreamWriter(fs) {AutoFlush = true};

            Console.SetOut(writer);

            if (NativeImport.GetConsoleMode(outFile, out var cMode))
                NativeImport.SetConsoleMode(outFile, cMode | 0x0200);

            Console.Title = $@"Debug Window - {Assembly.GetExecutingAssembly().GetName().Name}";
        }

        public static void DestroyConsole()
        {
            if (NativeImport.GetConsoleWindow() != IntPtr.Zero)
            {
                NativeImport.FreeConsole();
            }
            else
            {
                MessageBox.Show(null, @"Error: There is no debug console running!",
                    $@"{Assembly.GetExecutingAssembly().GetName().Name}");
                return;
            }
        }

        public static string Log(string format, LogLevel formatColor = LogLevel.Debug)
        {
            if (NativeImport.GetConsoleWindow() != IntPtr.Zero)
            {
                var consoleColour = Console.ForegroundColor;

                switch (formatColor)
                {
                    case LogLevel.Debug:
                        consoleColour = ConsoleColor.Cyan;
                        break;
                    case LogLevel.Error:
                        consoleColour = ConsoleColor.Red;
                        break;
                    case LogLevel.Warn:
                        consoleColour = ConsoleColor.Magenta;
                        break;
                    case LogLevel.Info:
                        break;
                    default:
                        // Default color
                        break;
                }

                Console.ForegroundColor = consoleColour;

                if (string.IsNullOrEmpty(format))
                {
                    Console.WriteLine(
                        $@"[{Assembly.GetExecutingAssembly().GetName().Name}] StringNullOrEmpty Occured at LogService.Log");
                    return
                        $"[{Assembly.GetExecutingAssembly().GetName().Name}] StringNullOrEmpty Occured at LogService.Log";
                }

                Console.WriteLine(
                    $@"[{DateTime.Now:h:mm:ss tt} - {Assembly.GetExecutingAssembly().GetName().Name}]: {format}");
                return $"[{DateTime.Now:h:mm:ss tt} - {Assembly.GetExecutingAssembly().GetName().Name}]: {format}";
            }
            else
            {
                MessageBox.Show(null, @"Error: There is no debug console running!",
                    $@"{Assembly.GetExecutingAssembly().GetName().Name}");
                return "Error: There is no debug console running!";
            }
        }

        public static void Clear()
        {
            if (NativeImport.GetConsoleWindow() != IntPtr.Zero)
            {
                Console.Clear();
            }
            else
            {
                MessageBox.Show(null, @"Error: There is no debug console running!",
                    $@"{Assembly.GetExecutingAssembly().GetName().Name}");
                return;
            }
        }

        public static void ShowConsole()
        {
            if (NativeImport.GetConsoleWindow() != IntPtr.Zero)
            {
                var handle = NativeImport.GetConsoleWindow();
                NativeImport.ShowWindow(handle, NativeImport.SW_SHOW);
            }
            else
            {
                MessageBox.Show(null, @"Error: There is no debug console running!",
                    $@"{Assembly.GetExecutingAssembly().GetName().Name}");
                return;
            }
        }

        public static void HideConsole()
        {
            if (NativeImport.GetConsoleWindow() != IntPtr.Zero)
            {
                var handle = NativeImport.GetConsoleWindow();
                NativeImport.ShowWindow(handle, NativeImport.SW_HIDE);
            }
            else
            {
                MessageBox.Show(null, @"Error: There is no debug console running!",
                    $@"{Assembly.GetExecutingAssembly().GetName().Name}");
                return;
            }
        }

        public static IntPtr GetConsoleHandle()
        {
            if (NativeImport.GetConsoleWindow() != IntPtr.Zero)
            {
                return NativeImport.GetConsoleWindow();
            }
            else
            {
                MessageBox.Show(null, @"Error: There is no debug console running!",
                    $@"{Assembly.GetExecutingAssembly().GetName().Name}");
                return IntPtr.Zero;
            }
        }
    }
}