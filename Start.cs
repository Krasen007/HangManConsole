/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan
{
    using System;

    public class Start
    {
        protected Start()
        {
        }

        public static void Main(string[] args)
        {
            PressKeyToStart();
            StartGame();
        }

        /// <summary>
        /// Start the game instance.
        /// </summary>
        private static void StartGame() => new Hangman();

        /// <summary>
        /// Press any key to start the game.
        /// </summary>
        private static void PressKeyToStart()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;
            switch (pid)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    {
                        Console.WriteLine("Welcome to HangManConsole for Windows");
                        break;
                    }

                case PlatformID.Unix:
                    Console.WriteLine("Welcome to HangManConsole for Linux");
                    break;
                case PlatformID.MacOSX:
                    Console.WriteLine("Welcome to HangManConsole for Mac!");
                    break;
                default:
                    Console.WriteLine("Welcome to HangManConsole for this OS");
                    break;
            }

            Console.WriteLine("\nPress any key to start\n");
            Console.ReadKey(intercept: true);
        }
    }
}