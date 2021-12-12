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
                if (isUpDiag(start, end)) return getUpDiagVector(start, end);
                // by the rules of the puzzle, there are only 4 possibilities,
                // leaving this one
                return getDownDiagVector(start, end);
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

            // Since the coordinates have the top left at [0, 0], up diags
            // go up and right.  Thus, any given point must either be the
            // bottom left of the diagonal or the top right
            bool isUpDiag(Point start, Point end)
            {
                return isStartBottomLeft(start, end) || isStartTopRight(start, end);
            }

            bool isStartBottomLeft(Point start, Point end)
            {
                return start.Y > end.Y && start.X < end.X;
            }

            bool isStartTopRight(Point start, Point end)
            {
                return start.Y < end.Y && start.X > end.X;
            }

            bool isStartBottomRight(Point start, Point end)
            {
                return start.Y > end.Y && start.X > end.X;
            }

            bool isStartTopLeft(Point start, Point end)
            {
                return start.Y < end.Y && start.X < end.X;
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

            // starts top left (min X, min Y)
            Vector getDownDiagVector(Point start, Point end)
            {
                Point bottomRight = isStartBottomRight(start, end) ? start : end;
                Point topLeft =  isStartBottomRight(start, end) ? end : end;
                if (isStartBottomRight(start, end))
                {
                    bottomRight = start;
                    topLeft = end;
                }
                else
                {
                    bottomRight = end;
                    topLeft = start;
                }
                int x0 = topLeft.X;
                int x1 = bottomRight.X;
                int y0 = topLeft.Y;
                int y1 = bottomRight.Y;
                Point[] points = new Point[x1 - x0 + 1];

                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new Point(x0 + i, y0 + i);
                }
                return new Vector(points);
            }

            // starts bottom left(min X, max Y)
            Vector getUpDiagVector(Point start, Point end)
            {
                Point bottomLeft = isStartBottomLeft(start, end) ? start : end;
                Point topRight = isStartBottomLeft(start, end) ? end : start;
                int x0 = bottomLeft.X;
                int x1 = topRight.X;
                int y0 = bottomLeft.Y;

                Point[] points = new Point[x1 - x0 + 1];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new Point(x0 + i, y0 - i);
                }
                return new Vector(points);
            }
        }
    }
}