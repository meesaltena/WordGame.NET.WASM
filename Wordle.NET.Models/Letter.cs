namespace Wordle.NET.Models
{
    public class Letter
    {
        public char letter;
        public Color color;
        public string cssClass => $"{CssColor()} {animation}";
        public string cssStyle { get; set; } = "";
        public string animation { get; set; } = "";

        public Letter(char l)
        {
            letter = l;

        }

        public void SetSpinAnimation()
        {
            cssStyle = "";
            animation = "spin-anim";
        }

        public void SetWinAnimation()
        {
            animation = "win-anim";
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
}
