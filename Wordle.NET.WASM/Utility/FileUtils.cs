using System.Reflection;

namespace Wordle.NET.WASM.Utility
{
    public class FileUtils
    {
        public List<string> GetWordsFromAssembly(string filename)
        {
            List<string> lines = new();
            string wordsResource = $"Wordle.NET.WASM.Words.{filename}";
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                {
                    using Stream stream = assembly.GetManifestResourceStream(wordsResource)!;
                    if (stream is not null)
                    {
                        using StreamReader streamReader = new(stream);
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine()!;
                            lines.Add(line.ToUpperInvariant());
                        }
                    } else
                    {
                        Console.WriteLine($"Error: Missing \"{wordsResource}\" embedded resource.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lines;
        }
    }
}
