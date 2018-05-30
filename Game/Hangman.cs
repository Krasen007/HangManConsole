/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan
{
    using System;
    using System.IO;

    public class Hangman
    {
        private const string DictFile = "Assets/wordsEn.txt";
        private const int NumberOfLives = 106;
        private const int MinLetterLength = 4;
        private readonly string[] wordDictionary;

        private int remainingLives;
        private bool isGameOver;
        private bool exitGame;
        private char[] currentGuess;
        private string allGuessedLetters;
        private string wrongLetters;
        private string correctLetters;
        private string selectedWord;

        public Hangman()
        {
            // Load game assets/files
            if (File.Exists(DictFile))
            {
                this.wordDictionary = File.ReadAllLines(DictFile);
                this.NewGame();
            }
            else
            {
                const string FileMissing = "Game dictionary file does not exist. Exiting...";
                Console.WriteLine(FileMissing);
                Console.ReadKey(true);
                this.ExitGame();
            }

            if (this.exitGame)
            {
                const string ExitString = "Have fun!";
                Console.WriteLine(ExitString);
            }
        }

        private void NewGame()
        {
            this.ResetValues(this.wordDictionary);
            this.GameLoop();
        }

        private void GameLoop()
        {
            // Game Loop
            while (!this.isGameOver)
            {
                this.DrawUi();

                // Input
                string guessedLetter = Console.ReadLine();

                // Main Logic
                if (this.IsInputCorrect(guessedLetter))
                {
                    this.UpdateLetters(guessedLetter);
                    this.allGuessedLetters = this.allGuessedLetters + guessedLetter + ' ';
                }

                // Game Over Condition
                if (this.remainingLives == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Game over! The correct word was: {0}", this.selectedWord);
                    this.isGameOver = true;
                }

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

            this.PlayAgain();
        }

        private void PlayAgain()
        {
            Console.Clear();
            const string PlayAgainText = "Do you want to play again?, type Y to play a new word, N to quit game.";
            Console.WriteLine(PlayAgainText);

            string playAgainLetter = Console.ReadLine();
            if (this.IsInputCorrect(playAgainLetter))
            {
                const string Yes = "Y";
                const string No = "N";
                if (playAgainLetter.ToUpper() == Yes)
                {
                    this.NewGame();
                }
                else if (playAgainLetter.ToUpper() == No)
                {
                    this.ExitGame();
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

        private bool ExitGame()
        {
            this.exitGame = true;
            return this.exitGame;
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

        private bool IsInputCorrect(string guessedLetter)
        {
            // Validating input
            if (guessedLetter.Length > 1 || guessedLetter.Length == 0)
            {
                Console.Clear();
                const string SingleLetter = "Wrong Input! Please, input a Single letter";
                Console.WriteLine(SingleLetter);
                Console.ReadKey(true);
                return false;
            }
            else if (!char.IsLetter(Convert.ToChar(guessedLetter)))
            {
                Console.Clear();
                const string WrongCharacter = "Wrong Input! Please, input a letter";
                Console.WriteLine(WrongCharacter);
                Console.ReadKey(true);
                return false;
            }

            return true;
        }

        private void DrawUi()
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

        private void ResetValues(string[] dictionary)
        {
            Random rng = new Random();
            this.selectedWord = string.Empty;

            // We need a word larger than 4 symbols
            do
            {
                int randomWordIndex = rng.Next(0, dictionary.Length);
                this.selectedWord = dictionary[randomWordIndex];
            }
            while (this.selectedWord.Length <= MinLetterLength);

            // Initialiazation
            this.remainingLives = NumberOfLives;
            this.currentGuess = new string('_', this.selectedWord.Length).ToCharArray();
            this.allGuessedLetters = string.Empty;
            this.wrongLetters = string.Empty;
            this.correctLetters = string.Empty;
            this.isGameOver = false;
        }
    }
}