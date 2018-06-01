/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan
{
    using System;
    using System.IO;
    using HangMan.States;

    public class Hangman
    {
        private const string DictFile = "Assets/wordsEn.txt";
        private readonly string[] wordDictionary;

        public Hangman()
        {
            if (File.Exists(DictFile))
            {
                this.wordDictionary = File.ReadAllLines(DictFile);
                this.StartMainMenu(this.wordDictionary);
                //// this.MainMenu();
            }
            else
            {
                const string FileMissing = "Game dictionary file does not exist. Exiting...";
                Console.WriteLine(FileMissing);
                Console.ReadKey(true);
                //// this.ExitGame();
            }
        }

        private void StartMainMenu(string[] wordDictionary)
        {
            MainMenu mainMenu = new MainMenu(wordDictionary);
        }
    }
}