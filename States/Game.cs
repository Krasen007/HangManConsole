/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan.States
{
    using System;
    using HangMan.Helper;

    public class Game
    {
        ////private int minLetterLength;
        ////private int numberOfLives;
        private readonly CheckInput checkInput = new CheckInput();

        private int remainingLives;
        private bool isGameOver;
        private char[] currentGuess;
        private string allGuessedLetters;
        private string wrongLetters;
        private string correctLetters;
        private string wordToGuess;

        ////private int minLetterLength;
        ////private int numberOfLives;
        ////public Game()
        ////{
        ////    this.NewGame();
        ////}

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
                this.CheckGameOver();
                this.CheckGameWin();
            }

            this.PlayAgain();
        }

        private void CheckGameWin()
        {
            // Game Win Condition
            string checkString = new string(this.currentGuess);
            if (!checkString.Contains("_"))
            {
                Console.Clear();
                Console.WriteLine("You won! The correct word was {0}", this.wordToGuess);
                this.isGameOver = true;
                Console.ReadKey(true);
            }
        }

        private void CheckGameOver()
        {
            // Game Over Condition
            if (this.remainingLives <= 0)
            {
                this.remainingLives = 0;
                Console.Clear();
                Console.WriteLine("Game over! The correct word was: {0}", this.wordToGuess);
                this.isGameOver = true;
            }
        }

        private void UpdateInput()
        {
            // Input
            string guessedLetter = Console.ReadLine();

            // Main Logic
            if (this.checkInput.IsInputCorrect(guessedLetter, false))
            {
                this.UpdateLetters(guessedLetter);
                this.allGuessedLetters = this.allGuessedLetters + guessedLetter + ' ';
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

        private void PlayAgain()
        {
            Console.Clear();
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
                    ////this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (playAgainLetter.ToUpper() == No)
                {
                    this.ExitGame();
                }
                else if (playAgainLetter.ToUpper() == Menu)
                {
                   //// new MainMenu();
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
    }
}
