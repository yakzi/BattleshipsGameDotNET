using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameDotNET.Controllers
{
    public class GameController : Controller
    {
        private void InsertShip(int shipLength, Direction direction, List<Point> Board)
        {
            var isValid = true;                                                                                         //To know if the ship can be placed or not (because of map size or other ship beeing already in here)
            var lengthX = Board.Max(point => point.X);
            var lengthY = Board.Max(point => point.Y);                                                                  //These two vars are the same to be honest (10x10 board), but for now with my 'thinking process' it will be easier to make kind of a duplicate
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
                                                                                                                        //SingleOrDefault to prevent nullpointer if this 'sector' will be out of the board..

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
                        var pointLeft = Board.Single(p => p.X == startX - i && p.Y == startY);      //Since the point is valid (checked before) there is no need to use SingleOrDefault
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
    }
}
