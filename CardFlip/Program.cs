using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#region "instructions"

//You're presented with a sequence of cards, some face up, 
//some face down. You can remove any face up card, but you 
//must then flip the adjacent cards (if any). The goal is 
//to successfully remove every card. Making the wrong move 
//can get you stuck.

#endregion

namespace CardFlip
{
    class Program
    {
        static string moves = string.Empty;

        public enum results
        {
            solved, notsolved, unsolvable
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a series of 1s and 0s:");
            string input = Console.ReadLine();

            if (FlipCards(input) == results.solved)
                Console.WriteLine(moves);
            else
                Console.WriteLine("Cannot be solved.");

            Console.ReadLine();
        }

        private static results FlipCards(string input)
        {
            StringBuilder s = new StringBuilder(input);

            //go through all cards flipping the cards that are faceup
            for (int i = 0; i < input.Length; i++)
            {


                //if card is faceup process it
                if (s[i] == '1')
                {

                    moves += i.ToString() + " ";

                    s[i] = '.';

                    if (i == 0)
                    {
                        //if it was card 0 that was flipped only flip card to the right
                        s[1] = FlipCard(s[1]);
                    }
                    else if (i == (input.Length - 1))
                    {
                        //if the last card was flipped only flip the card to the left
                        s[i - 1] = FlipCard(s[i - 1]);
                    }
                    else
                    {
                        //not the first or last card so flip card on either side
                        s[i - 1] = FlipCard(s[i - 1]);
                        s[i + 1] = FlipCard(s[i + 1]);
                    }

                    break;
                }
            }

            if (s.ToString().All(ch => ch == '.'))
            {
                //all cards were flipped; solved
                return results.solved;
            }
            else
            {
                if (Island(s.ToString()))
                {
                    return results.unsolvable;
                }
                else
                {
                    //not solved; call flipcards again
                    return FlipCards(s.ToString());
                }
            }


        }

        private static char FlipCard(char c)
        {
            //if card has not already been flipped flip it
            if (c != '.')
                return (c == '1') ? '0' : '1';

            return c;
        }

        private static bool Island(string s)
        {
            //check for isolated 0s that cannot be flipped
            Regex regx = new Regex(@"\.00\.|\.0$|$0\.");
            return regx.IsMatch(s);
        }

    }
}
