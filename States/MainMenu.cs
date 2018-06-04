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
        private Game game;
        
        public MainMenu()
        {
            string[] wordDictionary;
            wordDictionary = File.ReadAllLines(Constants.GameWordDictionary);

            this.SelectGameMode(wordDictionary);
        }

        private void SelectGameMode(string[] wordDictionary)
        {
            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words\n2: Medium words\n3: Long words\n4: Exit game";
            Console.WriteLine(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();

            int minLetterLength;
            int numberOfLives;

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
                    this.game = new Game(wordDictionary, numberOfLives, minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    minLetterLength = 4;
                    numberOfLives = 6;
                    this.game = new Game(wordDictionary, numberOfLives, minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    minLetterLength = 6;
                    numberOfLives = 3;
                    this.game = new Game(wordDictionary, numberOfLives, minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    //// this.ExitGame();
                    //// just exits the funcion.
                }
                else
                {
                    this.SelectGameMode(wordDictionary);
                }
            }
            else
            {
                this.SelectGameMode(wordDictionary);
            }
        }
    }
}
