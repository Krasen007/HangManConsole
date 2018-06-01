/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan
{
    using HangMan.Helper;
    using System;
    using System.IO;

    public class Hangman
    {
        private const string DictFile = "Assets/wordsEn.txt";

        private readonly string[] wordDictionary;

        private int minLetterLength;
        private int numberOfLives;
        private int remainingLives;
        private bool isGameOver;
        private char[] currentGuess;
        private string allGuessedLetters;
        private string wrongLetters;
        private string correctLetters;
        private string selectedWord;
        private CheckInput checkInput = new CheckInput();

        public Hangman()
        {
            // Load game assets/files
            if (File.Exists(DictFile))
            {
                this.wordDictionary = File.ReadAllLines(DictFile);
                this.MainMenu();
            }
            else
            {
                const string FileMissing = "Game dictionary file does not exist. Exiting...";
                Console.WriteLine(FileMissing);
                Console.ReadKey(true);
                this.ExitGame();
            }
        }

        private void MainMenu()
        {
            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words\n2: Medium words\n3: Long words\n4: Exit game";
            Console.WriteLine(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();

            if (checkInput.IsInputCorrect(pickDifficilty, true))
            {
                const string Easy = "1";
                const string Medium = "2";
                const string Hard = "3";
                const string Exit = "4";
                if (pickDifficilty.ToUpper() == Easy)
                {
                    this.minLetterLength = 3;
                    this.numberOfLives = 9;
                    this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    this.minLetterLength = 4;
                    this.numberOfLives = 6;
                    this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    this.minLetterLength = 6;
                    this.numberOfLives = 3;
                    this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    this.ExitGame();
                }
                else
                {
                    this.MainMenu();
                }
            }
            else
            {
                this.MainMenu();
            }
        }

        private void NewGame(int numberOfLives, int minLetterLength)
        {
            this.ResetValues(this.wordDictionary, numberOfLives, minLetterLength);
            this.GameLoop();
        }

        private void GameLoop()
        {
            while (!this.isGameOver)
            {
                this.UpdateUI();
                this.UpdateInput();
                this.CheckGameOver();
                this.CheckGameWin();
            }

            this.PlayAgain();
        }

        private void CheckGameWin()
        {
            // Game Win Condition
            string hackString = new string(this.currentGuess);
            if (!hackString.Contains("_"))
            {
                Console.Clear();
                Console.WriteLine("You won! The correct word was {0}", this.selectedWord);
                this.isGameOver = true;
                Console.ReadKey(true);
            }
        }

        private void CheckGameOver()
        {
            // Game Over Condition
            if (this.remainingLives == 0)
            {
                Console.Clear();
                Console.WriteLine("Game over! The correct word was: {0}", this.selectedWord);
                this.isGameOver = true;
            }
        }

        private void UpdateInput()
        {
            // Input
            string guessedLetter = Console.ReadLine();

            // Main Logic

            if (checkInput.IsInputCorrect(guessedLetter, false))
            {
                this.UpdateLetters(guessedLetter);
                this.allGuessedLetters = this.allGuessedLetters + guessedLetter + ' ';
            }
        }

        private void PlayAgain()
        {
            Console.Clear();
            const string PlayAgainText = "Do you want to play again?, type Y to play a new word, N to quit game.\nPress M to return to the menu.";
            Console.WriteLine(PlayAgainText);

            string playAgainLetter = Console.ReadLine();

            if (checkInput.IsInputCorrect(playAgainLetter, false))
            {
                const string Yes = "Y";
                const string No = "N";
                const string Menu = "M";
                if (playAgainLetter.ToUpper() == Yes)
                {
                    this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (playAgainLetter.ToUpper() == No)
                {
                    this.ExitGame();
                }
                else if (playAgainLetter.ToUpper() == Menu)
                {
                    this.MainMenu();
                }
                else
                {
                    this.PlayAgain();
                }
            }
            else
            {
                this.PlayAgain();
            }
        }

        private void ExitGame()
        {
            const string ExitString = "Have fun!";
            Console.WriteLine(ExitString);
        }

        private void UpdateLetters(string guessedLetter)
        {
            bool isLetterCorrect = false;

            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                if (this.selectedWord[i] == Convert.ToChar(guessedLetter))
                {
                    this.currentGuess[i] = Convert.ToChar(guessedLetter);
                    this.correctLetters = this.correctLetters + guessedLetter + ' ';
                    isLetterCorrect = true;
                }
            }

            // Taking lives
            if (!isLetterCorrect)
            {
                this.remainingLives--;
                this.wrongLetters = this.wrongLetters + guessedLetter + ' ';
            }
        }

        private void UpdateUI()
        {
            Console.Clear();

            // Drawing UI
            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                Console.Write("{0} ", this.currentGuess[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Number of Lives: {0}", this.remainingLives);
            Console.WriteLine();
            Console.WriteLine("All Guessed Letters: {0}", this.allGuessedLetters);
            Console.WriteLine("Wrong Letters: {0}", this.wrongLetters);
            Console.WriteLine("Correct Letters: {0}", this.correctLetters);
            const string InputLetter = "Please, input a letter: ";
            Console.Write(InputLetter);
        }

        private void ResetValues(string[] dictionary, int numberOfLives, int minLetterLength)
        {
            Random rng = new Random();
            this.selectedWord = string.Empty;

            // We need a word larger than 4 symbols
            do
            {
                int randomWordIndex = rng.Next(0, dictionary.Length);
                this.selectedWord = dictionary[randomWordIndex];
            }
            while (this.selectedWord.Length <= minLetterLength);

            // Initialiazation
            this.remainingLives = numberOfLives;
            this.currentGuess = new string('_', this.selectedWord.Length).ToCharArray();
            this.allGuessedLetters = string.Empty;
            this.wrongLetters = string.Empty;
            this.correctLetters = string.Empty;
            this.isGameOver = false;
        }
    }
}