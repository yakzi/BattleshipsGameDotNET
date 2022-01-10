using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameDotNET.Controllers
{
    public class GameController : Controller
    {
        public Player CreatePlayer()
        {
            return new Player();
        }
        private void CreateBoard(Player player)
        {
            player.Board = new List<Point>();
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    player.Board.Add(new Point
                    {
                        Field = Field.Empty,
                        X = x,
                        Y = y
                    });
                }
            }
        }
    }
}
