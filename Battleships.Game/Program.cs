namespace Battleships.Game
{
    // Throughout: exception handling to be added/fuinaliosed. Appropriate exception handling strategies
    // will emerge from development.
    internal class Program
    {
        // State and configuration could be put in storage (local, cookies etc) enabling users to pause games
        // or save preferred set ups.
        static void Main(string[] args)
        {
            // Give opportunities for user customisation.
            var battleship = new ShipType { Name = "Battleship", Size = 5 };
            var destroyer = new ShipType { Name = "Destroyer", Size = 4 };

            var ships = new List<Ship>();

            // Move ship generation to its own service to enable testing, this either external to or
            // within the Game class - scope for user customisation. 
            // Number of ships and their characteristics can be set in configuration or by user input.
            var battleship1 = new Ship { Type = battleship };
            var destroyer1 = new Ship { Type = destroyer };
            var destroyer2 = new Ship { Type = destroyer };

            ships.Add(battleship1);
            ships.Add(destroyer1);
            ships.Add(destroyer2);

            var game = new Game(10, ships, 12);

            Console.WriteLine("*** Battleships ***");
            Console.WriteLine();

            // Final degree of encapsulation of functionality within the Game class TBD with ref to distribution channels.
            while (!game.GameOver)
            {
                Console.Write("Fire: ");
                var shot = Console.ReadLine();
                // Add validation here. Use an external service e.g. "IInputValidator" to enable testing.
                Console.WriteLine(game.Fire(shot));
                Console.WriteLine();
            }

            Console.WriteLine("*** Game Over! ***");
            Console.WriteLine();
            game.PrintBoard();
        }
    }
}
