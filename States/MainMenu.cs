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
        private int minLetterLength = Constants.DefaultLetterLength;
        private int numberOfLives = Constants.DefaultLives;

        public MainMenu()
        {
            this.DrawMenuUi();
        }

        private void DrawMenuUi()
        {
            Console.Clear();
            string menuText = "Main menu\n" +
                "1: New game\n" +
                "2: Settings\n" +
                "3: Exit\n" +
                "Current settings: Long letter words: " + this.minLetterLength + " lives: " + this.numberOfLives + "\n" +
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
                    this.PickGameDictionary();
                }
                else if (pickMenuItem.ToUpper() == Settings)
                {
                    this.ShowSettings();
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

        private void PickGameDictionary()
        {
            Console.Clear();
            const string GameDifficultyText = "Select Game Dictionary:\n" +
                "1: Animals, 2: Random words" +
                "\nYour pick: ";
            Console.Write(GameDifficultyText);
            string pickGameDictionary = Console.ReadLine();
            string[] wordDictionary = { };

            if (this.checkInput.IsInputCorrect(pickGameDictionary, true))
            {
                const string Animals = "1";
                const string AllWords = "2";
                if (pickGameDictionary.ToUpper() == Animals)
                {
                    wordDictionary = File.ReadAllLines(Constants.AnimalsDictionary);
                }
                else if (pickGameDictionary.ToUpper() == AllWords)
                {
                    wordDictionary = File.ReadAllLines(Constants.AllWordsDictionary);
                }
                else
                {
                    this.PickGameDictionary();
                }
            }
            else
            {
                this.PickGameDictionary();
            }

            ///return wordDictionary;

            string[] defaultDictionary = wordDictionary;

            if (this.minLetterLength != Constants.DefaultLives)
            {
                this.SelectedNewGame(wordDictionary, this.minLetterLength, this.numberOfLives);
            }
            else
            {
                this.DefaultNewGame(defaultDictionary, Constants.DefaultLetterLength, Constants.DefaultLives);
            }
        }

        /// <summary>
        /// Lets the user select game mode.
        /// </summary>
        private void ShowSettings()
        {
            bool wantToExit = false;

            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words, 9 lives\n2: Medium words, 6 lives\n3: Long words, 3 lives\n4: Exit game\nYour pick: ";
            Console.Write(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(pickDifficilty, true))
            {
                const string Easy = "1";
                const string Medium = "2";
                const string Hard = "3";
                const string Exit = "4";
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
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    wantToExit = true;
                }
                else
                {
                    this.ShowSettings();
                }
            }
            else
            {
                this.ShowSettings();
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

        private void ShowExitString()
        {
            Console.Clear();
            const string ExitString = "Thank you for playing!";
            Console.WriteLine(ExitString);
        }

        private void SelectedNewGame(string[] wordDictionary, int minLetterLength, int numberOfLives)
            => new Game(wordDictionary, numberOfLives, minLetterLength);

        private void DefaultNewGame(string[] defaultDictionary, int defaultLetterLength, int defaultLives)
            => new Game(defaultDictionary, defaultLives, defaultLetterLength);
    }
}
