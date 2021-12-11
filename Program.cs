namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            var lines = System.IO.File.ReadAllLines(@".\input.txt");
            var len = lines[0].Length;

            bool isOneMostCommon(int[] lines, int position)
            {
                var (zero, one) = countBits(lines, position);
                return one >= zero;
            }

            int[] filtered = lines.Select(x => Convert.ToInt32(x, fromBase: 2)).ToArray();
            for (int i = len - 1; i >= 0; i--)
            {
                filtered = filterByRequiredBit(filtered, i, isOneMostCommon);
            }

            int oxygen = filtered[0];

            bool isOneLeastCommon(int[] lines, int position) => !isOneMostCommon(lines, position);

            filtered = lines.Select(x => Convert.ToInt32(x, fromBase: 2)).ToArray();
            for (int i = len - 1; i >= 0; i--)
            {
                filtered = filterByRequiredBit(filtered, i, isOneLeastCommon);
            }

            int cO2 = filtered[0];

            Console.WriteLine(oxygen * cO2);


            bool hasOne(int num, int position)
            {
                return (num >> (position) & 1) == 1;
            }

            (int zero, int one) countBits(int[] lines, int n)
            {
                int zero = 0;
                int one = 0;
                foreach (var num in lines)
                {
                    if (hasOne(num, n))
                    {
                        one++;
                    }
                    else
                    {
                        zero++;
                    }
                }
                return (zero, one);
            }

            int[] filterByRequiredBit(int[] lines, int n, Func<int[], int, bool> findFilterBit)
            {
                if (lines.Length == 1) return lines;
                Func<int, int,bool> predicate = findFilterBit(lines, n) ? (x, n) => hasOne(x, n) : (x, n) => !hasOne(x, n);
                return lines.Where(x => predicate(x, n)).ToArray();
        }

    }
}
}