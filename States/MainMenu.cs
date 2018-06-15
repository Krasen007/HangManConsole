/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan.States
{
    using System;
    using System.IO;
    using HangMan.Helper;

    public class MainMenu
    {
        private readonly CheckInput checkInput = new CheckInput();
        private int minLetterLength;
        private int numberOfLives;
        private bool firstLastLtrShown;
        private bool customGame;

        public MainMenu(int minLetterLength, int numberOfLives, bool firstLastLtrShown)
        {
            this.minLetterLength = minLetterLength;
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
                "Minimium word letter length: " + this.minLetterLength + 
                "\nNumber of lives: " + this.numberOfLives +
                "\nFirst and last letter of word shown: " + this.firstLastLtrShown + "\n\n" +
                "Your pick: ";
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
            const string GameDifficultyText = "Select Game Dictionary:\n" +
                "1: Animals\n" +
                "2: Random words\n" +
                "3: Exit" +
                "\nYour pick: ";
            Console.Write(GameDifficultyText);
            string pickGameDictionary = Console.ReadLine();
            string[] wordDictionary = { };
            bool wantToExit = false;

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
                    this.SelectedNewGame(wordDictionary, this.minLetterLength, this.numberOfLives, this.firstLastLtrShown);
                }
                else
                {
                    this.DefaultNewGame(wordDictionary, Constants.DefaultLetterLength, Constants.DefaultLives, true);
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
                "Your pick: ";
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
                    this.numberOfLives = 9;
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    this.minLetterLength = 4;
                    this.numberOfLives = 6;
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    this.minLetterLength = 6;
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

        private void SelectedNewGame(string[] wordDictionary, int minLetterLength, int numberOfLives, bool firstLastLtrShown)
            => new Game(wordDictionary, numberOfLives, minLetterLength, firstLastLtrShown);

        private void DefaultNewGame(string[] defaultDictionary, int defaultLetterLength, int defaultLives, bool firstLastLtrShown)
            => new Game(defaultDictionary, defaultLives, defaultLetterLength, firstLastLtrShown);
    }
}
