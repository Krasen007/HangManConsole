/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using HangMan.Helper;
    using HangMan.States;

    public class Hangman
    {
        private readonly List<string> gameDictFile = new List<string>();

        public Hangman()
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(Constants.FolderPath))
                {
                    this.gameDictFile.Add(file);
                }
            }
            catch (IOException ex)
            {
                const string FileMissing = "Game Assets directory does not exist. Exiting... ";
                Console.WriteLine(FileMissing);
                Console.WriteLine(ex.Message);
                Console.ReadKey(intercept: true);
                Console.Clear();
                throw;
            }

            this.LoadAllNeededFiles(this.gameDictFile);
        }

        private void LoadAllNeededFiles(List<string> neededFiles)
        {
            if (neededFiles.Count == 0)
            {
                const string FileMissing = "No game dictionary file exist. Try adding a few. " +
                    "\nThe game will now exit... ";
                Console.WriteLine(FileMissing);
                Console.ReadKey(intercept: true);
                Console.Clear();
            }
            else
            {
                this.StartMainMenu();
            }
        }

        private void StartMainMenu() => new MainMenu(this.gameDictFile, Constants.DefaultMinLetterLength, Constants.DefaultMaxLetterLength, Constants.DefaultLives, true);
    }
}