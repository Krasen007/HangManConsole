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
        public Hangman()
        {
            List<bool> files = new List<bool>
            {
                File.Exists(Constants.AllWordsDictionary),
                File.Exists(Constants.AnimalsDictionary)
            };

            this.LoadAllNeededFiles(files);
        }

        private void LoadAllNeededFiles(List<bool> neededFiles)
        {
            if (neededFiles[0] && neededFiles[1])
            {
                this.StartMainMenu();
            }
            else
            {
                const string FileMissing = "Game dictionary file does not exist. Exiting...";
                Console.WriteLine(FileMissing);
                Console.ReadKey(intercept: true);
            }
        }

        private void StartMainMenu() => new MainMenu();
    }
}