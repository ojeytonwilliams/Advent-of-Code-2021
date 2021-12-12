using System.Text.Json;

namespace Utils
{
    class Format
    {
        public static void PrettyPrint<T>(T xs)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(xs, options);
            Console.WriteLine(jsonString);
        }

    }
}