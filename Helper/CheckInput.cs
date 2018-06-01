namespace HangMan.Helper
{
    using System;

    public class CheckInput
    {
        public bool IsInputCorrect(string guessedInput, bool isNumber)
        {
            try
            {
                if (!isNumber && (!char.IsLetter(Convert.ToChar(guessedInput))))
                {
                    Console.Clear();
                    const string WrongCharacter = "Wrong Input! Please, input a letter";
                    Console.WriteLine(WrongCharacter);
                    Console.ReadKey(true);
                    return false;
                }
                else if (guessedInput.Length > 1 || guessedInput.Length == 0)
                {
                    Console.Clear();
                    const string SingleLetter = "Wrong Input! Please, input a Single string";
                    Console.WriteLine(SingleLetter);
                    Console.ReadKey(true);
                    return false;
                }
                else if (isNumber && char.IsLetter(Convert.ToChar(guessedInput)))
                {
                    Console.Clear();
                    const string WrongCharacter = "Wrong Input! Please, input a Number for the selected difficulty";
                    Console.WriteLine(WrongCharacter);
                    Console.ReadKey(true);
                    return false;
                }
                else if (guessedInput == " ")
                {
                    Console.Clear();
                    const string WrongCharacter = "Wrong Input! Please, no empty strings";
                    Console.WriteLine(WrongCharacter);
                    Console.ReadKey(true);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                Console.Clear();
                const string WrongMultiGuess = "Guess with only one character!";
                Console.WriteLine(WrongMultiGuess);
                Console.ReadKey(true);
                return false;
            }
        }
    }
}
