namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

    string[] lines = System.IO.File.ReadAllLines(@".\input.txt");
    int[] xs = lines.Select(x => Convert.ToInt32(x)).ToArray();
            int previous = xs[0];
            int count = 0;
            foreach (int i in xs)
            {
                if (i > previous) {
                    count++;
                }
                previous = i;
            }
            Console.Write(count);
        }

    }
}