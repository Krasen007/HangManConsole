/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan.States
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using HangMan.Helper;

    public class MainMenu
    {
        private readonly CheckInput checkInput = new CheckInput();
        private readonly List<string> gameFiles;
        private int minLetterLength;
        private int maxLetterLength;
        private int numberOfLives;
        private bool firstLastLtrShown;
        private bool customGame;

        public MainMenu(List<string> gameFiles, int minLetterLength, int maxLetterLength, int numberOfLives, bool firstLastLtrShown)
        {
            this.gameFiles = gameFiles;
            this.minLetterLength = minLetterLength;
            this.maxLetterLength = maxLetterLength;
            this.numberOfLives = numberOfLives;
            this.firstLastLtrShown = firstLastLtrShown;
            this.DrawMenuUi();
        }

        /// <summary>
        /// Draws the Ui of the menu.
        /// </summary>
        private void DrawMenuUi()
        {
            Console.Clear();
            string menuText = "Main menu\n" +
                "1: New game\n" +
                "2: Settings\n" +
                "3: Exit\n\n" +
                "Current settings: \n" +
                "Min/Max word letter length is: " + this.minLetterLength + " to " + this.maxLetterLength +
                "\nNumber of lives: " + this.numberOfLives +
                "\nFirst and last letter of word shown: " + this.firstLastLtrShown + "\n\n" +
                "Enter number for selection: ";
            Console.Write(menuText);

            string pickMenuItem = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(pickMenuItem, true))
            {
                const string NewGame = "1";
                const string Settings = "2";
                const string Exit = "3";
                if (pickMenuItem.ToUpper() == NewGame)
                {
                    this.NewGameMenu();
                }
                else if (pickMenuItem.ToUpper() == Settings)
                {
                    this.customGame = true;
                    this.SettingsMenu();
                }
                else if (pickMenuItem.ToUpper() == Exit)
                {
                    this.ShowExitString();
                }
                else
                {
                    this.DrawMenuUi();
                }
            }
            else
            {
                this.DrawMenuUi();
            }
        }

        /// <summary>
        /// Used to select game dictionary.
        /// </summary>
        private void NewGameMenu()
        {
            Console.Clear();
            string menuText = "Select game dictionary:";
            Console.WriteLine(menuText);

            int menuIndex = this.gameFiles.Count;

            for (int i = 0; i < menuIndex; i++)
            {
                Console.WriteLine(i + 1 + ": " + this.gameFiles[i].Remove(0, 7));
            }

            string settingsText = menuIndex + 1 + ": Exit" +
                "\nEnter number for selection: ";
            Console.Write(settingsText);

            string pickGameDictionary = Console.ReadLine();
            string[] wordDictionary = { };
            bool wantToExit = false;

            // FIX: Next part to do the multiple input files...
            if (this.checkInput.IsInputCorrect(pickGameDictionary, true))
            {
                const string Animals = "1";
                const string AllWords = "2";
                const string Exit = "3";
                if (pickGameDictionary.ToUpper() == Animals)
                {
                    wordDictionary = File.ReadAllLines(Constants.AnimalsDictionary);
                }
                else if (pickGameDictionary.ToUpper() == AllWords)
                {
                    wordDictionary = File.ReadAllLines(Constants.AllWordsDictionary);
                }
                else if (pickGameDictionary.ToUpper() == Exit)
                {
                    wantToExit = true;
                }
                else
                {
                    this.NewGameMenu();
                }
            }
            else
            {
                this.NewGameMenu();
            }

            if (wantToExit)
            {
                this.ShowExitString();
            }
            else
            {
                if (this.customGame)
                {
                    this.SelectedNewGame(gameFiles, wordDictionary, this.minLetterLength, this.maxLetterLength, this.numberOfLives, this.firstLastLtrShown);
                }
                else
                {
                    this.DefaultNewGame(gameFiles, wordDictionary, Constants.DefaultMinLetterLength, Constants.DefaultMaxLetterLength, Constants.DefaultLives, true);
                }
            }
        }

        /// <summary>
        /// Lets the user select game mode.
        /// </summary>
        private void SettingsMenu()
        {
            bool wantToExit = false;

            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words, 9 lives\n" +
                "2: Medium words, 6 lives\n" +
                "3: Long words, 3 lives\n" +
                "4: Enable/disable first and last letter of word shown\n" +
                "5: Exit game\n" +
                "Enter number for selection: ";
            Console.Write(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(pickDifficilty, true))
            {
                const string Easy = "1";
                const string Medium = "2";
                const string Hard = "3";
                const string ShowFirstLastLtrs = "4";
                const string Exit = "5";
                if (pickDifficilty.ToUpper() == Easy)
                {
                    this.minLetterLength = 3;
                    this.maxLetterLength = 5;
                    this.numberOfLives = 9;
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    this.minLetterLength = 4;
                    this.maxLetterLength = 7;
                    this.numberOfLives = 6;
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    this.minLetterLength = 6;
                    this.maxLetterLength = 10;
                    this.numberOfLives = 3;
                }
                else if (pickDifficilty.ToUpper() == ShowFirstLastLtrs)
                {
                    if (this.firstLastLtrShown)
                    {
                        this.firstLastLtrShown = false;
                    }
                    else
                    {
                        this.firstLastLtrShown = true;
                    }
                }
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    wantToExit = true;
                }
                else
                {
                    this.SettingsMenu();
                }
            }
            else
            {
                this.SettingsMenu();
            }

            if (wantToExit)
            {
                this.ShowExitString();
            }
            else
            {
                this.DrawMenuUi();
            }
        }

        /// <summary>
        /// Displays exit text.
        /// </summary>
        private void ShowExitString()
        {
            Console.Clear();
            const string ExitString = "Thank you for playing!\n\nPress any key to exit... ";
            Console.Write(ExitString);
            Console.ReadKey(intercept: true);
        }

        private void SelectedNewGame(List<string> gameFiles, string[] wordDictionary, int minLetterLength, int maxLetterLength, int numberOfLives, bool firstLastLtrShown)
            => new Game(gameFiles, wordDictionary, numberOfLives, minLetterLength, maxLetterLength, firstLastLtrShown);

        private void DefaultNewGame(List<string> gameFiles, string[] defaultDictionary, int defaultMinLetterLength, int defaultMaxLetterLength, int defaultLives, bool firstLastLtrShown)
            => new Game(gameFiles, defaultDictionary, defaultLives, defaultMinLetterLength, defaultMaxLetterLength, firstLastLtrShown);
    }
}
