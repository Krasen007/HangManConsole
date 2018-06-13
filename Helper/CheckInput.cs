/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Krasen Ivanov. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace HangMan.Helper
{
    using System;

    public class CheckInput
    {
        public bool IsInputCorrect(string guessedInput, bool isNumber)
        {
            try
            {
                if (guessedInput.Length > 1 || guessedInput.Length <= 0)
                {
                    Console.Clear();
                    const string WrongCharacter = "Wrong Input! Please, input a single letter";
                    Console.WriteLine(WrongCharacter);
                    Console.ReadKey(true);
                    return false;
                }
                else if (!isNumber && (!char.IsLetter(Convert.ToChar(guessedInput))))
                {
                    Console.Clear();
                    const string WrongCharacter = "Wrong Input! Please, input a letter";
                    Console.WriteLine(WrongCharacter);
                    Console.ReadKey(true);
                    return false;
                }                
                else if (isNumber && char.IsLetter(Convert.ToChar(guessedInput)))
                {
                    Console.Clear();
                    const string WrongCharacter = "Wrong Input! Please, input a number";
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
            catch (Exception n)
            {
                Console.Clear();
                const string WrongMultiGuess = "FormatException Execption!";
                Console.WriteLine(WrongMultiGuess);
                Console.WriteLine(n.Message);
                Console.ReadKey(true);
                return false;
            }
        }
    }
}
