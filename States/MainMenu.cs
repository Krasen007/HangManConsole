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
            this.SelectGameMode(this.PickGameDictionary());
        }

        private string[] PickGameDictionary()
        {
            Console.Clear();
            const string GameDifficultyText = "Select Game Dictionary:\n" +
                "1: Animals, 2: Random words" +
                "\nType the number: ";
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
                    this.SelectGameMode(this.PickGameDictionary());
                }
            }
            else
            {
                this.SelectGameMode(this.PickGameDictionary());
            }

            return wordDictionary;
        }

        /// <summary>
        /// Lets the user select game mode.
        /// </summary>
        private void SelectGameMode(string[] wordDictionary)
        {
            string[] defaultDictionary = wordDictionary;

            const int DefaultLetterLength = 3;
            const int DefaultLives = 9;

            int minLetterLength = DefaultLetterLength;
            int numberOfLives = DefaultLives;
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
                    this.SelectGameMode(defaultDictionary);
                }
            }
            else
            {
                this.SelectGameMode(defaultDictionary);
            }

            if (wantToExit)
            {
                const string ExitString = "Bye.";
                Console.WriteLine(ExitString);
            }
            else
            {
                if (minLetterLength != DefaultLives)
                {
                    this.SelectedNewGame(wordDictionary, minLetterLength, numberOfLives);
                }
                else
                {
                    this.DefaultNewGame(defaultDictionary, DefaultLetterLength, DefaultLives);
                }
            }
        }

        private void SelectedNewGame(string[] wordDictionary, int minLetterLength, int numberOfLives)
            => new Game(wordDictionary, numberOfLives, minLetterLength);

        private void DefaultNewGame(string[] defaultDictionary, int defaultLetterLength, int defaultLives)
            => new Game(defaultDictionary, defaultLives, defaultLetterLength);
    }
}
