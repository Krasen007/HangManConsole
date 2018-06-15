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
        private readonly bool firstLastLtrShown;

        private int remainingLives;
        private bool isGameOver;
        private char[] currentGuess;
        private string allGuessedLetters;
        private string wrongLetters;
        private string correctLetters;
        private string wordToGuess;

        public Game(string[] wordDictionary, int numberOfLives, int minLetterLength, bool firstLastLtrShown)
        {
            this.currentWordDictionary = wordDictionary;
            this.numberOfLives = numberOfLives;
            this.minLetterLength = minLetterLength;
            this.firstLastLtrShown = firstLastLtrShown;

            this.NewGame(this.currentWordDictionary, this.numberOfLives, this.minLetterLength, this.firstLastLtrShown);
        }

        public void NewGame(string[] wordDictionary, int numberOfLives, int minLetterLength, bool firstLastLtrShown)
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
            this.allGuessedLetters = string.Empty;
            this.wrongLetters = string.Empty;
            this.correctLetters = string.Empty;
            this.isGameOver = false;

            if (this.firstLastLtrShown)
            {
                this.currentGuess = new string('_', this.wordToGuess.Length).ToCharArray();
                this.currentGuess[0] = this.wordToGuess[0];
                this.currentGuess[this.currentGuess.Length - 1] = this.wordToGuess[this.wordToGuess.Length - 1];
            }
            else
            {
                this.currentGuess = new string('_', this.wordToGuess.Length).ToCharArray();
            }

            this.GameLoop();
        }

        /// <summary>
        /// Updated every frame.
        /// </summary>
        public void GameLoop()
        {
            while (!this.isGameOver)
            {
                this.UpdateUi();
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
                string wonText = $"You won! The correct word was {this.wordToGuess}\nPress any key to continue...\n";
                Console.WriteLine(wonText);
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
                string gameOverText = $"Game over! The correct word was: {this.wordToGuess}\n";
                Console.WriteLine(gameOverText);
                this.isGameOver = true;
            }
        }

        /// <summary>
        /// Checks if the input is correct and displays it.
        /// </summary>
        private void UpdateInput()
        {
            string guessedLetter = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(guessedLetter, false))
            {
                this.UpdateLetters(guessedLetter);
            }
        }

        /// <summary>
        /// Redraws the UI every frame.
        /// </summary>
        private void UpdateUi()
        {
            Console.Clear();

            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                string emptyStringText = $"{this.currentGuess[i]} ";
                Console.Write(emptyStringText);
            }

            const string Separator = " ";
            string numberOfLivesText = $"Number of Lives: {this.remainingLives}";
            string allGuessedLettersText = $"All Guessed Letters: {this.allGuessedLetters}";
            string wrongLettersText = $"Wrong Letters: {this.wrongLetters}";
            string correctLettersText = $"Correct Letters: {this.correctLetters}";
            const string InputLetter = "Please, input a letter: ";

            Console.WriteLine(Separator);
            Console.WriteLine(numberOfLivesText);
            Console.WriteLine();
            Console.WriteLine(allGuessedLettersText);
            Console.WriteLine(wrongLettersText);
            Console.WriteLine(correctLettersText);
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

        /// <summary>
        /// Displays information after a game to play again.
        /// </summary>
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
                    this.NewGame(this.currentWordDictionary, this.numberOfLives, this.minLetterLength, this.firstLastLtrShown);
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

        /// <summary>
        /// Starts the main menu
        /// </summary>
        private void GoToMainMenu() => new MainMenu(this.minLetterLength, this.numberOfLives, this.firstLastLtrShown);

        /// <summary>
        /// Exit message.
        /// </summary>
        private void ExitGame()
        {
            const string ExitString = "Have fun!";
            Console.WriteLine(ExitString);
        }
    }
}
