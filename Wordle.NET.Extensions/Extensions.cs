using Color = Wordle.NET.Models.Color;

namespace Wordle.NET.Wasm.Extensions
{
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

    public static class EnumExtensions
    {
        public static string ToHex(this Color color)
        {
            int colorValue = (int)color;
            return "#" + (colorValue & 0xFFFFFF).ToString("X6");
        }
    }
}
