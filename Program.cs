namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            var lines = System.IO.File.ReadAllLines(@".\input.txt");
            var len = lines[0].Length;
            int[] zeros = new int[len];
            int[] ones = new int[len];
            foreach (string line in lines)
            {
                int num = Convert.ToInt32(line, fromBase: 2);
                for (int i = 0; i < len; i++)
                {
                    if ((num >> (len - 1 - i) & 1) == 1)
                    {
                        ones[i]++;
                    }
                    else
                    {
                        zeros[i]++;
                    }
                }
            }

            int gamma = 0;
            int epsilon = 0;
            for (int i = 0; i < len; i++)
            {
                int delta = Convert.ToInt32(Math.Pow(2, (len - 1 - i)));
                if (ones[i] > zeros[i])
                {
                    gamma += delta;
                }
                else
                {
                    epsilon += delta;
                }
            }
            
            Console.WriteLine(gamma);
            Console.WriteLine(epsilon);
            Console.WriteLine(gamma * epsilon);
        }
    }
}