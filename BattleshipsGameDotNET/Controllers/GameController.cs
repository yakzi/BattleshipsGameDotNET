using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameDotNET.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Play()
        {
            List<Player> players = new List<Player>();
            var p1 = CreatePlayer();
            var p2 = CreatePlayer();
            CreateBoard(p1);
            CreateBoard(p2);
            players.Add(p1);
            players.Add(p2);
            InsertStartingShips(players);

            while (p1.Score < Consts.MaxScore || p2.Score < Consts.MaxScore)                      
            {
                Fire(p1, p2);
                if (p1.Score == Consts.MaxScore) break;
                Fire(p2, p1);
                if (p2.Score == Consts.MaxScore) break;
            }
            return View((p1, p2));
        }
        private void InsertShip(int shipLength, Direction direction, List<Point> Board)
        {
            var isValid = true;                                                                                         //To know if the ship can be placed or not (because of map size or other ship beeing already in here)
            var lengthX = Board.Max(point => point.X);
            var lengthY = Board.Max(point => point.Y);                                                                  
            int startX, startY;                                                                                         //For keeping the ship 'drawing' starting point
            do
            {
                startX = new Random().Next(0, lengthX + 1);
                startY = new Random().Next(0, lengthY + 1);                                                             //Random starting coorditanes of new ship

                if (Board.Single(p => p.X == startX && p.Y == startY).Field != Field.Empty)                             //This if will check, if field on which we try to place new ship is empty
                {
                    isValid = false;
                    continue;
                }

                isValid = true;
                for (var i = 0; i < shipLength; i++)
                {
                    switch (direction)
                    {
                        case Direction.Left:
                            var currentLeftPoint = Board.SingleOrDefault(p => p.X == startX - i && p.Y == startY);      //Getting current 'sector' of ship on board
                            if (currentLeftPoint is null ||
                                currentLeftPoint.Field != Field.Empty ||
                                Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) + 1 && p.Y == currentLeftPoint.Y) is null ||
                                Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) - 1 && p.Y == currentLeftPoint.Y) is null ||
                                Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y + 1) is null ||
                                Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y - 1) is null ||
                                Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) + 1 && p.Y == currentLeftPoint.Y).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) - 1 && p.Y == currentLeftPoint.Y).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y + 1).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y - 1).Field == Field.ShipPlaced)

                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Right:
                            var currentRightPoint = Board.SingleOrDefault(p => p.X == startX + i && p.Y == startY);
                            if (currentRightPoint is null ||
                                currentRightPoint.Field != Field.Empty ||
                                Board.SingleOrDefault(p => p.X == (currentRightPoint.X) + 1 && p.Y == currentRightPoint.Y) is null ||
                                Board.SingleOrDefault(p => p.X == (currentRightPoint.X) - 1 && p.Y == currentRightPoint.Y) is null ||
                                Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y + 1) is null ||
                                Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y - 1) is null ||
                                Board.SingleOrDefault(p => p.X == (currentRightPoint.X) + 1 && p.Y == currentRightPoint.Y).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == (currentRightPoint.X) - 1 && p.Y == currentRightPoint.Y).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y + 1).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Up:
                            var currentUpPoint = Board.SingleOrDefault(p => p.X == startX && p.Y == startY + i);
                            if (currentUpPoint is null || currentUpPoint.Field != Field.Empty ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X + 1 && p.Y == (currentUpPoint.Y)) is null ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X - 1 && p.Y == (currentUpPoint.Y)) is null ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) + 1) is null ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) - 1) is null ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X + 1 && p.Y == (currentUpPoint.Y)).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X - 1 && p.Y == (currentUpPoint.Y)).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) + 1).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Down:
                            var currentDownPoint = Board.SingleOrDefault(p => p.X == startX && p.Y == startY - i);
                            if (currentDownPoint is null || currentDownPoint.Field != Field.Empty ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X + 1 && p.Y == (currentDownPoint.Y)) is null ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X - 1 && p.Y == (currentDownPoint.Y)) is null ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) + 1) is null ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) - 1) is null ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X + 1 && p.Y == (currentDownPoint.Y)).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X - 1 && p.Y == (currentDownPoint.Y)).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) + 1).Field == Field.ShipPlaced ||
                                Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                    }
                }

            } while (!isValid);

            for (var i = 0; i < shipLength; i++)                                                    //Actual ship insertion to the board
            {
                switch (direction)
                {
                    case Direction.Left:
                        var pointLeft = Board.Single(p => p.X == startX - i && p.Y == startY);      
                        pointLeft.Field = Field.ShipPlaced;
                        break;
                    case Direction.Right:
                        var pointRight = Board.Single(p => p.X == startX + i && p.Y == startY);
                        pointRight.Field = Field.ShipPlaced;
                        break;
                    case Direction.Up:
                        var pointUp = Board.Single(p => p.X == startX && p.Y == startY + i);
                        pointUp.Field = Field.ShipPlaced;
                        break;
                    case Direction.Down:
                        var pointDown = Board.Single(p => p.X == startX && p.Y == startY - i);
                        pointDown.Field = Field.ShipPlaced;
                        break;
                }
            }
        }
        private void InsertStartingShips(List<Player> players)
        {
            foreach (var player in players)
            {
                InsertShip(5, (Direction)new Random().Next(0, 4), player.Board);
                InsertShip(4, (Direction)new Random().Next(0, 4), player.Board);
                InsertShip(3, (Direction)new Random().Next(0, 4), player.Board);
                InsertShip(2, (Direction)new Random().Next(0, 4), player.Board);
                InsertShip(2, (Direction)new Random().Next(0, 4), player.Board);
                InsertShip(1, (Direction)new Random().Next(0, 4), player.Board);
                InsertShip(1, (Direction)new Random().Next(0, 4), player.Board);
            }
        }
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
        public void Fire(Player shooter, Player target)
        {
            if (shooter.Score < Consts.MaxScore)                                                                 
            {
                int x, y;
                do
                {
                    x = new Random().Next(0, 10);                                                  
                    y = new Random().Next(0, 10);
                } while (!target.Board.Any(p => p.X == x && p.Y == y && p.Field != Field.Miss && p.Field != Field.ShipHit));      //because 'AI' knows previous shots 

                var aimedPoint = target.Board.First(p => p.X == x && p.Y >= y);                                                   //actual shooting point
                if (aimedPoint.Field != Field.ShipHit && aimedPoint.Field != Field.Miss)
                {
                    if (aimedPoint.Field == Field.ShipPlaced)
                    {
                        aimedPoint.Field = Field.ShipHit;
                        shooter.Hits.Add(aimedPoint);
                        shooter.Score++;
                    }
                    else
                    {
                        aimedPoint.Field = Field.Miss;
                        shooter.Hits.Add(aimedPoint);
                    }
                }
            }
        }
    }
}
