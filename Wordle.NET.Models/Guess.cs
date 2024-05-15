namespace Wordle.NET.Models
{
    public class Guess
    {
        public List<Letter> letters;
        private string guess;
        private string target;

        public Guess(string guessWord, string targetWord)
        {
            guess = guessWord;
            target = targetWord;
            letters = new();

            for (int i = 0; i < guessWord.Length; i++)
            {
                char c = guessWord[i];
                Letter l = new Letter(c);

                // make text transparent https://stackoverflow.com/a/10835846
                l.cssStyle = "color: rgba(0, 0, 0, 0.0);";

                l.color = Color.Wrong;
                letters.Add(l);
            }
        }

        public bool IsCorrect()
        {
            return letters.All(l => l.color == Color.Correct);
        }

        public void SetWinAnimation(int letterNo)
        {
            letters[letterNo].SetWinAnimation();
        }

        public void SetSpinAnimation(int letterNo)
        {
            letters[letterNo].SetSpinAnimation();
        }
        public void SetLetterColor(int letterNo)
        {

            if (target[letterNo] == letters[letterNo])
            {
                letters[letterNo].color = Color.Correct;
            }
            else if (CheckForYellow(letterNo, target, letters.Select(l => l.letter).ToArray()))
            {
                letters[letterNo].color = Color.Close;

            }
            else
            {
                letters[letterNo].color = Color.Wrong;
            }

        }

        // Taken from https://github.com/dotnet/dotnet-console-games
        private bool CheckForYellow(int index, string word, char[] letters)
        {
            int letterCount = 0;
            int incorrectCountBeforeIndex = 0;
            int correctCount = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letters[index])
                {
                    letterCount++;
                }
                if (letters[i] == letters[index] && word[i] == letters[index])
                {
                    correctCount++;
                }
                if (i < index && letters[i] == letters[index] && word[i] != letters[index])
                {
                    incorrectCountBeforeIndex++;
                }
            }
            return letterCount - correctCount - incorrectCountBeforeIndex > 0;
        }

        public Letter this[int index]
        {
            get => letters[index];
        }

        public override string ToString()
        {
            string o = "";
            foreach (Letter l in letters)
            {
                o += l.ToString();
            }
            return o;
        }
    }
}
