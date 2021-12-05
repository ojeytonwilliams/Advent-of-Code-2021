namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@".\input.txt");
            int[] xs = lines.Select(x => Convert.ToInt32(x)).ToArray();
            int previous = xs[0] + xs[1] + xs[2];
            int count = 0;

            for (int i = 3; i < xs.Length; i++)
            {
                int sum = xs[i - 2] + xs[i - 1] + xs[i];
                if (sum > previous) count++;
                previous = sum;
            }
            Console.Write(count);
        }

    }
}