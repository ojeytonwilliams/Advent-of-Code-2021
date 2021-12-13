namespace HelloWorld
{
    using static Utils.Format;
    class Program
    {
        static void Main(string[] args)
        {

            var lines = System.IO.File.ReadAllLines(@".\input.txt");
            int[] initialState = lines[0].Split(",").Select(s => Convert.ToInt32(s)).ToArray();


            // 'tests'
            // Console.WriteLine(numberOfFish(2, 2)); // 1
            // Console.WriteLine(numberOfFish(2, 3)); // 2
            // Console.WriteLine(numberOfFish(1, 1)); // 1
            // Console.WriteLine(numberOfFish(1, 8)); // 2
            // Console.WriteLine(numberOfFish(1, 9)); // 3
            Dictionary<int, long> memo = new();
            // long fish = (new int[] { 3, 4, 3, 1, 2 }).Select(state => numberOfFish(state, 256)).Sum();

            long fish = initialState.Select(x => numberOfFish(x, 256 )).Sum();

            Console.WriteLine(fish);

            long numberOfFish(int state, int days)
            {
                return 1 + childrenProduced(days - state);
            }

            long childrenProduced(int days)
            {
                // if less than 1 days left it can't do anything
                // day 1 it produces a child (state 8) and goes to state 6
                // that means we can call childrenProduced( days - 7) and childrenProduced(days - 9) and we're done
                if (days < 1) return 0;
                long stored = -1;
                if (memo.TryGetValue(days, out stored))
                {
                    return stored;
                }
                else
                {
                    long result = 1 + childrenProduced(days - 7) + childrenProduced(days - 9);
                    memo[days] = result;
                    return result;
                }


            }
        }
    }
}