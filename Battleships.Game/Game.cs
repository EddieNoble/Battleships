namespace Battleships.Game
{
    public class Game
    {
        public bool GameOver => _attempts == 0;

        private int _boardSize;

        private List<Ship> _ships;

        private int _attempts;

        private char[,]? _board;

        private List<Tuple<int, int>> _misses = new List<Tuple<int, int>>();

        private List<Tuple<int, int>> _hits = new List<Tuple<int, int>>();

        private List<Tuple<int, int>> _occupiedSquares = new List<Tuple<int, int>>();

        public Game(int boardSize, List<Ship> ships, int attempts)
        {
            _boardSize = boardSize;
            _ships = ships;

            _board = GenerateBoard();
            _attempts = attempts;
        }

        public string Fire(string shot)
        {
            string message = "";
            var coordArr = shot.ToCharArray();
            var row = Utilities.GetLetterIndex(coordArr[0]);
            var col = int.Parse(coordArr[1].ToString());
            var coords = new Tuple<int, int>(row, col);
            if (!_occupiedSquares.Contains(coords))
            {
                _misses.Add(coords);
                message = $"Miss! You have {_attempts - 1} shots remaining.";
            }
            else
            {
                foreach (var ship in _ships)
                {
                    if (ship.Coordinates.Contains(coords))
                    {
                        _hits.Add(coords);
                        ship.Hits++;
                        message =
                            ship.Hits == ship.Type.Size
                            ? $"Congratulations! You sunk a {ship.Type.Name}."
                            : $"Hit! You have {_attempts - 1} shots remaining.";
                        break;
                    }
                }
            }
            _attempts--;
            return message;
        }

        // Would be developed next to return a collection of strings to enable testing independently
        // of the game. Later development would make the return value more generic so the consuming
        // code has more control over output.
        public void PrintBoard()
        {
            char printChar;

            foreach (var ship in _ships)
            {
                printChar = ship.Type.Name.ToString().Substring(0, 1)[0];
                foreach (var coord in ship.Coordinates)
                {
                    _board[coord.Item1, coord.Item2] = printChar;
                }
            }

            foreach (var hit in _hits)
            {
                printChar = 'X';
                _board[hit.Item1, hit.Item2] = printChar;
            }

            foreach (var miss in _misses)
            {
                printChar = '0';
                _board[miss.Item1, miss.Item2] = printChar;
            }

            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col] == '\0')
                    {
                        Console.Write("~ ");
                    }
                    else
                    {
                        Console.Write($"{_board[row, col]} ");
                    }
                }
                Console.WriteLine();
            }
        }

        private char[,] GenerateBoard()
        {
             var board = new char[_boardSize, _boardSize];

            var coordinates = new List<Tuple<int, int>>();

            foreach (var ship in _ships)
            {
                coordinates = ObjectPlotter.GetRange2d(ship.Type.Size, 10, _occupiedSquares);
                ship.Coordinates = coordinates;
                _occupiedSquares.AddRange(coordinates);
            }

            return board;
        }
    }
}
