namespace Battleships.Game
{
    public class Ship
    {
        public ShipType? Type { get; set; }

        public List<Tuple<int, int>> Coordinates { get; set; } = new List<Tuple<int, int>>();

        public int Hits { get; set; } = 0;
    }
}
