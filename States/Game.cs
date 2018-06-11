/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan.States
{
    using System;
    using System.Text;
    using HangMan.Helper;

    public class Game
    {
        private readonly CheckInput checkInput = new CheckInput();
        private readonly string[] currentWordDictionary;
        private readonly int numberOfLives;
        private readonly int minLetterLength;

        private int remainingLives;
        private bool isGameOver;
        private char[] currentGuess;
        private string allGuessedLetters;
        private string wrongLetters;
        private string correctLetters;
        private string wordToGuess;

        public Game(string[] wordDictionary, int numberOfLives, int minLetterLength)
        {
            this.currentWordDictionary = wordDictionary;
            this.numberOfLives = numberOfLives;
            this.minLetterLength = minLetterLength;

            this.NewGame(this.currentWordDictionary, this.numberOfLives, this.minLetterLength);
        }

        public void NewGame(string[] wordDictionary, int numberOfLives, int minLetterLength)
        {
            Random rng = new Random();
            this.wordToGuess = string.Empty;

            do
            {
                int pickRandomWord = rng.Next(0, wordDictionary.Length);
                this.wordToGuess = wordDictionary[pickRandomWord];
            }
            while (this.wordToGuess.Length <= minLetterLength);

            // Initialiazation
            this.remainingLives = numberOfLives;
            this.currentGuess = new string('_', this.wordToGuess.Length).ToCharArray();
            this.allGuessedLetters = string.Empty;
            this.wrongLetters = string.Empty;
            this.correctLetters = string.Empty;
            this.isGameOver = false;

            this.GameLoop();
        }

        public void GameLoop()
        {
            while (!this.isGameOver)
            {
                this.UpdateUI();
                this.UpdateInput();
                this.CheckGameWin();
                this.CheckGameOver();
            }

            this.PlayAgain();
        }

        /// <summary>
        /// Game Win Condition.
        /// </summary>
        private void CheckGameWin()
        {
            string checkString = new string(this.currentGuess);
            if (!checkString.Contains("_"))
            {
                Console.Clear();
                Console.WriteLine("You won! The correct word was {0}\nPress any key to continue...\n", this.wordToGuess);
                this.isGameOver = true;
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Game Over Condition.
        /// </summary>
        private void CheckGameOver()
        {
            if (this.remainingLives <= 0)
            {
                this.remainingLives = 0;
                Console.Clear();
                Console.WriteLine("Game over! The correct word was: {0}\n", this.wordToGuess);
                this.isGameOver = true;
            }
        }

        private void UpdateInput()
        {
            string guessedLetter = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(guessedLetter, false))
            {
                this.UpdateLetters(guessedLetter);
            }
        }

        private void UpdateUI()
        {
            Console.Clear();

            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                Console.Write("{0} ", this.currentGuess[i]);
            }

            Console.WriteLine(" " + this.wordToGuess);
            Console.WriteLine("Number of Lives: {0}", this.remainingLives);
            Console.WriteLine();
            Console.WriteLine("All Guessed Letters: {0}", this.allGuessedLetters);
            Console.WriteLine("Wrong Letters: {0}", this.wrongLetters);
            Console.WriteLine("Correct Letters: {0}", this.correctLetters);
            const string InputLetter = "Please, input a letter: ";
            Console.Write(InputLetter);
        }

        private void UpdateLetters(string guessedLetter)
        {
            bool isLetterCorrect = false;

            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                if (this.wordToGuess[i] == Convert.ToChar(guessedLetter))
                {
                    this.currentGuess[i] = Convert.ToChar(guessedLetter);

                    StringBuilder correctLettersBuilder = new StringBuilder(this.correctLetters + guessedLetter + ' ');
                    if (!this.correctLetters.Contains(guessedLetter))
                    {
                        this.correctLetters = correctLettersBuilder.ToString();
                    }

                    isLetterCorrect = true;
                }
            }

            // Taking lives
            StringBuilder wrongLettersBuilder = new StringBuilder(this.wrongLetters + guessedLetter + ' ');
            if (!isLetterCorrect && !this.wrongLetters.Contains(guessedLetter))
            {
                this.remainingLives--;
                this.wrongLetters = wrongLettersBuilder.ToString();
            }

            StringBuilder allGuessedLettersBuilder = new StringBuilder(this.allGuessedLetters + guessedLetter + ' ');
            if (!this.allGuessedLetters.Contains(guessedLetter))
            {
                this.allGuessedLetters = allGuessedLettersBuilder.ToString();
            }
        }

        private void PlayAgain()
        {
            const string PlayAgainText = "Do you want to play again?, type Y to play a new word, N to quit game.\nPress M to return to the menu.";
            Console.WriteLine(PlayAgainText);

            string playAgainLetter = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(playAgainLetter, false))
            {
                const string Yes = "Y";
                const string No = "N";
                const string Menu = "M";
                if (playAgainLetter.ToUpper() == Yes)
                {
                    this.NewGame(this.currentWordDictionary, this.numberOfLives, this.minLetterLength);
                }
                else if (playAgainLetter.ToUpper() == No)
                {
                    this.ExitGame();
                }
                else if (playAgainLetter.ToUpper() == Menu)
                {
                    this.GoToMainMenu();
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

        private void GoToMainMenu() => new MainMenu();

        private void ExitGame()
        {
            const string ExitString = "Have fun!";
            Console.WriteLine(ExitString);
        }
    }
}
