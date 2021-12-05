﻿namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@".\input.txt");
            int forward = 0;
            int depth = 0;
            int aim = 0;
            foreach (string line in lines)
            {
                int amount = Convert.ToInt32(line.Split(" ")[1]);
                if (line[0] == "f"[0])
                {
                    forward += amount;
                    depth += aim * amount;
                }
                if (line[0] == "u"[0]) aim -= amount;
                if (line[0] == "d"[0]) aim += amount;
            }
            Console.Write(forward * depth);
        }

    }
}