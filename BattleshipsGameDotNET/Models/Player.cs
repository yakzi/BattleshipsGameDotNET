namespace BattleshipsGameDotNET.Models
{
    public class Player
    {
        public int Score;
        public List<Point> Hits = new List<Point>();
        public List<Point> Board { get; set; }
    }
}
