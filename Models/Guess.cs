namespace Wordle.NET.WASM.Models
{
    public class Guess
    {
        public List<Letter> letters;
        public string guess;
        public string target;


        public Guess(string guessWord, string targetWord)
        {
            guess = guessWord;
            target = targetWord;
            letters = guessWord.ToCharArray().Select(l => new Letter(l)).ToList();
            SetLetterColors();
        }

        private void SetLetterColors()
        {
            for (int i = 0; i < letters.Count(); i++)
            {
                Letter letter = letters[i];


                List<int> matchIndexes = target.AllIndexOf(letter); // list of positions in target where the current letter appears.
                

                if (matchIndexes.Count() == 0)
                {
                    letter.color = Color.Wrong;
                    continue;
                }

                if (matchIndexes.Any(m => m == i))
                {
                    matchIndexes.RemoveAll(m => m == i); // remove from matchlist
                    letter.color = Color.Correct;
                    continue;
                }

                foreach(int l in matchIndexes)
                {
                    letters[l].color = Color.Close;
                }

                letter.color = Color.Wrong;

            }
        }



        public Letter this[int index]
        {
            get => letters[index];
        }
    }

    public class Letter
    {
        public char letter;
        public Color color;

        public Letter(char l)
        {
            letter = l;
        }

        public string CssColor()
        {
            return $"cell color-{color.ToString().ToLower()}";
        }

        public static implicit operator char(Letter letter) => letter.letter;

        public override string? ToString()
        {
            return letter.ToString();
        }
    }

    public enum Color : uint
    {
        Background = 0x121312,
        Wrong = 0x3A3A3C,
        Correct = 0x538D4E,
        Close = 0xB59F3B,
        None = 0xFF6A00,
        Pink = 0xFF00DC
    }


    public static class StringExtension
    {
        public static List<int> AllIndexOf(this string text, string str, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            List<int> allIndexOf = new List<int>();
            int index = text.IndexOf(str, comparisonType);
            while (index != -1)
            {
                allIndexOf.Add(index);
                index = text.IndexOf(str, index + 1, comparisonType);
            }
            return allIndexOf;
        }

        public static List<int> AllIndexOf(this string text, char c, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            return AllIndexOf(text, c.ToString(), comparisonType);
        }
    }
}
