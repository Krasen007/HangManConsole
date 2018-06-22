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
            List<bool> files = new List<bool>();            

            try
            {
                foreach (var file in Directory.EnumerateFiles(Constants.FolderPath))
                {
                    if (File.Exists(file))
                    {
                        files.Add(true);
                        gameDictFile.Add(file);
                    }
                }
            }
            catch (IOException ex)
            {
                const string FileMissing = "Game dictionary file does not exist. Exiting... ";
                Console.WriteLine(FileMissing);
                Console.WriteLine(ex.Message);
                throw;
            }

            this.LoadAllNeededFiles(files);
        }

        private void LoadAllNeededFiles(List<bool> neededFiles)
        {
            if (neededFiles.Contains(false))
            {
                const string FileMissing = "Game dictionary file does not exist. Exiting... ";
                Console.WriteLine(FileMissing);
                Console.ReadKey(intercept: true);
            }
            else
            {
                this.StartMainMenu();
            }
        }

        private void StartMainMenu() => new MainMenu(gameDictFile, Constants.DefaultMinLetterLength, Constants.DefaultMaxLetterLength, Constants.DefaultLives, true);
    }
}