namespace HelloWorld
{
    using static Utils.Format;
    class Program
    {


        class Vector
        {
            // NOTE: Yeah, we could easily have kept the positions abstract and
            // just inferred if a given point was on the vector line or not.
            // This is silly, but here we are!
            public Point[] Positions { get; }

            public Point[] EndPoints()
            {
                return new Point[] { Positions.First(), Positions.Last() };
            }

            public Vector(Point[] points)
            {
                Positions = points;
            }
        }

        class Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int[] coords)
            {
                X = coords[0];
                Y = coords[1];
            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        static void Main(string[] args)
        {

            var lines = System.IO.File.ReadAllLines(@".\input.txt");
            string[][] vectorsRaw = lines.Select(x => x.Split("->", StringSplitOptions.TrimEntries)).ToArray();

            Vector[] vectors = vectorsRaw.Select(x => parseRawVector(x)).Where(v => v != null).ToArray()!;

            (int width, int height) = getDimensions(vectors);

            int[][] board = new int[width + 1][];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new int[height + 1];
            }

            foreach (var v in vectors)
            {
                addPoints(board, v);
            }

            int count = 0;

            foreach (var column in board)
            {
                foreach (var x in column)
                {
                    if (x >= 2) count++;
                }
            }

            Console.WriteLine(count);

            void addPoints(int[][] board, Vector vector)
            {
                foreach (var point in vector.Positions)
                {
                    board[point.X][point.Y]++;
                }
            }


            Console.WriteLine($"{width}, {height}");

            (int, int) getDimensions(Vector[] vectors)
            {
                int maxHeight = 0, maxWidth = 0;
                foreach (var v in vectors)
                {
                    var endPoint = v.EndPoints().Last();
                    int width = endPoint.X;
                    int height = endPoint.Y;
                    maxWidth = width > maxWidth ? width : maxWidth;
                    maxHeight = height > maxHeight ? height : maxHeight;
                }
                return (maxWidth, maxHeight);
            }

            Vector? parseRawVector(string[] xs)
            {
                Point start = parsePosition(xs[0]);
                Point end = parsePosition(xs[1]);
                if (isHorizontal(start, end)) return getHorizontalVector(start, end);
                if (isVertical(start, end)) return getVerticalVector(start, end);
                return null;
            }



            Point parsePosition(string s)
            {
                var coords = s.Split(",").Select(x => Convert.ToInt32(x)).ToArray();
                return new Point(coords);
            }

            bool isHorizontal(Point start, Point end)
            {
                return start.Y == end.Y;
            }

            bool isVertical(Point start, Point end)
            {
                return start.X == end.X;
            }

            Vector getHorizontalVector(Point start, Point end)
            {
                int y = start.Y;
                int x0 = start.X < end.X ? start.X : end.X;
                int x1 = start.X < end.X ? end.X : start.X;
                Point[] points = new Point[x1 - x0 + 1];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new Point(x0 + i, y);
                }
                return new Vector(points);
            }

            Vector getVerticalVector(Point start, Point end)
            {
                int x = start.X;
                int y0 = start.Y < end.Y ? start.Y : end.Y;
                int y1 = start.Y < end.Y ? end.Y : start.Y;
                Point[] points = new Point[y1 - y0 + 1];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new Point(x, y0 + i);
                }
                return new Vector(points);
            }

            // PrettyPrint(vectorsRaw);

            //     string[] boardData = new string[
            //         lines.Length - 1
            //     ];
            //     Array.Copy(lines, 1, boardData, 0, lines.Length - 1);
            //     IEnumerator<string> lineEnumerator = boardData.AsEnumerable().GetEnumerator();
            //     var boardStack = new Stack<int[][]>();

            //     while (lineEnumerator.MoveNext())
            //     {
            //         boardStack.Push(parseBoards(lineEnumerator));
            //     }

            //     var boards = boardStack.ToArray();

            //     int remainingBoards = boards.Length;
            //     HashSet<int> winners = new();
            //     foreach (int n in numbers)
            //     {
            //         for (int i = 0; i < boards.Length; i++)
            //         {
            //             if (winners.Contains(i)) continue;
            //             var board = boards[i];
            //             markPosition(board, n);
            //             if (isWinner(board))
            //             {
            //                 winners.Add(i);
            //                 remainingBoards--;
            //                 if (remainingBoards == 0)
            //                 {
            //                     Console.WriteLine(getScore(board, n));
            //                     Environment.Exit(0);
            //                 }
            //             }
            //         }
            //     }

            //     int getScore(int[][] board, int num)
            //     {
            //         int score = 0;
            //         foreach (var row in board)
            //         {
            //             foreach (int n in row)
            //             {
            //                 if (n > 0)
            //                 {
            //                     score += n;
            //                 }
            //             }
            //         }
            //         return score * num;
            //     }

            //     void prettyPrint<T>(T[][] board)
            //     {
            //         foreach (var item in board)
            //         {
            //             Console.WriteLine(String.Join(",", item));
            //         }
            //     }



            //     void markPosition(int[][] board, int n)
            //     {
            //         for (int i = 0; i < board.Length; i++)
            //         {
            //             var id = Array.IndexOf(board[i], n);
            //             if (id > -1)
            //             {
            //                 board[i][id] = -1;
            //             }
            //         }
            //     }

            //     bool isWinner(int[][] board)
            //     {
            //         if (hasFullRow(board)) return true;
            //         if (hasFullColumn(board)) return true;
            //         return false;
            //     }

            //     bool hasFullRow(int[][] board)
            //     {
            //         foreach (var item in board)
            //         {
            //             if (Array.TrueForAll(item, (int x) => x == -1))
            //             {
            //                 return true;
            //             }
            //         }
            //         return false;
            //     }

            //     bool hasFullColumn(int[][] board)
            //     {
            //         for (int col = 0; col < board.Length; col++)
            //         {
            //             bool full = true;
            //             for (int row = 0; row < board.Length; row++)
            //             {
            //                 if (board[row][col] > -1)
            //                 {
            //                     full = false;
            //                 }
            //             }
            //             if (full) return true;
            //         }
            //         return false;
            //     }



            //     int[][] parseBoards(IEnumerator<string> linesEnumerator)
            //     {
            //         int[][] board = new int[5][];
            //         for (int i = 0; i < 5 && linesEnumerator.MoveNext(); i++)
            //         {
            //             var strings = linesEnumerator.Current.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            //             board[i] = strings.Select(x => Convert.ToInt32(x)).ToArray();
            //         }
            //         return board;
            //     }

        }
    }
}