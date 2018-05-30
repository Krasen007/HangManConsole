/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan
{
    using System;

    public class Start
    {
        public static void Main(string[] args)
        {
            WelcomeText();
            PressKeyToStart();
            StartGame();
        }

        /// <summary>
        /// Start the game instance.
        /// </summary>
        private static void StartGame()
        {
            Hangman game = new Hangman();
        }

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
                        Console.ReadKey(true);
                        break;
                    }

                case PlatformID.Unix:
                    Console.WriteLine("Handle Linux");
                    Console.ReadLine();
                    break;
                case PlatformID.MacOSX:
                    Console.WriteLine("Handle Mac!");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Handle this OS");
                    Console.ReadLine();
                    break;
            }
        }

        /// <summary>
        /// Display Welcome text to the user.
        /// </summary>
        private static void WelcomeText()
        {
            const string WelcomeText = "Welcome to the Hangman game!\n\nPress any key to start";
            Console.WriteLine(WelcomeText);
        }
    }
}