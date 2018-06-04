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

        public MainMenu()
        {
            this.SelectGameMode();
        }

        private void SelectGameMode()
        {
            string[] wordDictionary;
            wordDictionary = File.ReadAllLines(Constants.GameWordDictionary);
            string[] defaultDictionary = wordDictionary;

            const int defaultLetterLength = 3;
            const int defaultLives = 9;

            int minLetterLength = defaultLetterLength;
            int numberOfLives = defaultLives;
            bool wantToExit = false;

            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words\n2: Medium words\n3: Long words\n4: Exit game";
            Console.WriteLine(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(pickDifficilty, true))
            {
                const string Easy = "1";
                const string Medium = "2";
                const string Hard = "3";
                const string Exit = "4";
                if (pickDifficilty.ToUpper() == Easy)
                {
                    minLetterLength = 3;
                    numberOfLives = 9;
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    minLetterLength = 4;
                    numberOfLives = 6;
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    minLetterLength = 6;
                    numberOfLives = 3;
                }
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    wantToExit = true;
                }
                else
                {
                    this.SelectGameMode();
                }
            }
            else
            {
                this.SelectGameMode();
            }

            if (wantToExit)
            {
                const string ExitString = "Bye.";
                Console.WriteLine(ExitString);
            }
            else
            {
                if (minLetterLength != defaultLives)
                {
                    this.SelectedNewGame(wordDictionary, minLetterLength, numberOfLives);
                }
                else
                {
                    this.DefaultNewGame(defaultDictionary, defaultLetterLength, defaultLives);
                }
            }
        }

        private void SelectedNewGame(string[] wordDictionary, int minLetterLength, int numberOfLives)
            => new Game(wordDictionary, numberOfLives, minLetterLength);

        private void DefaultNewGame(string[] defaultDictionary, int defaultLetterLength, int defaultLives)
            => new Game(defaultDictionary, defaultLives, defaultLetterLength);
    }
}
