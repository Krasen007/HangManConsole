namespace HangMan.States
{
    using System;

    public class MainMenu
    {
        public MainMenu()
        {
            Console.Clear();
            const string SelectDifficulty = "Select difficulty:\n1: Short words\n2: Medium words\n3: Long words\n4: Exit game";
            Console.WriteLine(SelectDifficulty);

            string pickDifficilty = Console.ReadLine();
            /*// this.IsInputCorrect(pickDifficilty, true)*/
            if (true) 
            {
                const string Easy = "1";
                const string Medium = "2";
                const string Hard = "3";
                const string Exit = "4";
                if (pickDifficilty.ToUpper() == Easy)
                {
                    // this.minLetterLength = 3;
                    // this.numberOfLives = 9;
                    //  this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Medium)
                {
                    // this.minLetterLength = 4;
                    // this.numberOfLives = 6;
                    // this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Hard)
                {
                    // this.minLetterLength = 6;
                    // this.numberOfLives = 3;
                    // this.NewGame(this.numberOfLives, this.minLetterLength);
                }
                else if (pickDifficilty.ToUpper() == Exit)
                {
                    // this.ExitGame();
                }
            }
            else
            {
                // this.MainMenu();
            }
        }
    }
}
