namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            var lines = System.IO.File.ReadAllLines(@".\input.txt");
            int[] numbers = lines[0].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            string[] boardData = new string[
                lines.Length - 1
            ];
            Array.Copy(lines, 1, boardData, 0, lines.Length - 1);
            IEnumerator<string> lineEnumerator = boardData.AsEnumerable().GetEnumerator();
            var boardStack = new Stack<int[][]>();

            while (lineEnumerator.MoveNext())
            {
                boardStack.Push(parseBoards(lineEnumerator));
            }

            var boards = boardStack.ToArray();

            int remainingBoards = boards.Length;
            HashSet<int> winners = new();
            foreach (int n in numbers)
            {
                for (int i = 0; i < boards.Length; i++)
                {
                    if (winners.Contains(i)) continue;
                    var board = boards[i];
                    markPosition(board, n);
                    if (isWinner(board))
                    {
                        winners.Add(i);
                        remainingBoards--;
                        if (remainingBoards == 0)
                        {
                            Console.WriteLine(getScore(board, n));
                            Environment.Exit(0);
                        }
                    }
                }
            }

            int getScore(int[][] board, int num)
            {
                int score = 0;
                foreach (var row in board)
                {
                    foreach (int n in row)
                    {
                        if (n > 0)
                        {
                            score += n;
                        }
                    }
                }
                return score * num;
            }

            void prettyPrint<T>(T[][] board)
            {
                foreach (var item in board)
                {
                    Console.WriteLine(String.Join(",", item));
                }
            }



            void markPosition(int[][] board, int n)
            {
                for (int i = 0; i < board.Length; i++)
                {
                    var id = Array.IndexOf(board[i], n);
                    if (id > -1)
                    {
                        board[i][id] = -1;
                    }
                }
            }

            bool isWinner(int[][] board)
            {
                if (hasFullRow(board)) return true;
                if (hasFullColumn(board)) return true;
                return false;
            }

            bool hasFullRow(int[][] board)
            {
                foreach (var item in board)
                {
                    if (Array.TrueForAll(item, (int x) => x == -1))
                    {
                        return true;
                    }
                }
                return false;
            }

            bool hasFullColumn(int[][] board)
            {
                for (int col = 0; col < board.Length; col++)
                {
                    bool full = true;
                    for (int row = 0; row < board.Length; row++)
                    {
                        if (board[row][col] > -1)
                        {
                            full = false;
                        }
                    }
                    if (full) return true;
                }
                return false;
            }



            int[][] parseBoards(IEnumerator<string> linesEnumerator)
            {
                int[][] board = new int[5][];
                for (int i = 0; i < 5 && linesEnumerator.MoveNext(); i++)
                {
                    var strings = linesEnumerator.Current.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    board[i] = strings.Select(x => Convert.ToInt32(x)).ToArray();
                }
                return board;
            }

        }
    }
}