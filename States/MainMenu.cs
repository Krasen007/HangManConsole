namespace HangMan.States
{
    using System;
    using HangMan.Helper;

    public class MainMenu
    {
        private readonly CheckInput checkInput = new CheckInput();
        private readonly Game game = new Game();

        private int minLetterLength;
        private int numberOfLives;

        public MainMenu(string[] wordDictionary)
        {
            this.SelectGameMode(wordDictionary);
        }

        private void SelectGameMode(string[] wordDictionary)
        {
            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words\n2: Medium words\n3: Long words\n4: Exit game";
            Console.WriteLine(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();

            if (this.checkInput.IsInputCorrect(pickDifficilty, true))
            {
                const string Easy = "1";
                const string Medium = "2";
                const string Hard = "3";
                const string Exit = "4";
                if (pickDifficilty.ToUpper() == Easy)
                {
                    this.minLetterLength = 3;
                    this.numberOfLives = 9;
                    this.game.NewGame(wordDictionary, this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    this.minLetterLength = 4;
                    this.numberOfLives = 6;
                    this.game.NewGame(wordDictionary, this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    this.minLetterLength = 6;
                    this.numberOfLives = 3;
                    this.game.NewGame(wordDictionary, this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    // this.ExitGame();
                    // just exits the funcion.
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
