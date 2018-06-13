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
            DefineOsAndStart();
        }

        /// <summary>
        /// Start the game instance.
        /// </summary>
        private static void StartGame() => new Hangman();

        /// <summary>
        /// Press any key to start the game.
        /// </summary>
        private static void DefineOsAndStart()
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
                        const string WelcomeWindows = "Welcome to HangManConsole for Windows";
                        Console.WriteLine(WelcomeWindows);
                        break;
                    }

                case PlatformID.Unix:
                    const string WelcomeLinux = "Welcome to HangManConsole for Linux";
                    Console.WriteLine(WelcomeLinux);
                    break;
                case PlatformID.MacOSX:
                    const string WelcomeMac = "Welcome to HangManConsole for Mac!";
                    Console.WriteLine(WelcomeMac);
                    break;
                default:
                    const string WelcomeAnyOS = "Welcome to HangManConsole for any OS";
                    Console.WriteLine(WelcomeAnyOS);
                    break;
            }

            const string AnyKeyStartText = "\nPress any key to start";
            Console.WriteLine(AnyKeyStartText);
            Console.ReadKey(intercept: true);
            StartGame();
        }
    }
}