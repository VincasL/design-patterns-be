//using BattleshipsApi.Entities;
//using BattleshipsApi.Facades;
//using BattleshipsApi.Entities;
//using BattleshipsApi.Enums;
//using BattleshipsApi.Strategies;
//using Microsoft.AspNetCore.SignalR;
//using SignalRSwaggerGen.Attributes;

//namespace BattleshipsApi.Template
//{
//    [SignalRHub]
//    public abstract class TemplateMethod : Hub
//    {
//        private readonly BattleshipsFacade _battleshipsFacade;
//        private readonly BattleshipHub _battleshipHub;

//        public TemplateMethod(BattleshipsFacade battleshipsFacade, BattleshipHub battleshipHub)
//        {
//            _battleshipsFacade = battleshipsFacade;
//            _battleshipHub = battleshipHub;
//        }
//        public virtual void PlaceObject(CellCoordinates cellCoordinates, ShipType type)
//        {
//            if (isShip())
//            {
//                var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
//                var player = session.GetPlayerByConnectionId(Context.ConnectionId);
//                var board = player.Board;

//                var factory = _battleshipsFacade.Factory;
//                var ship = factory.CreateShip(type, player.nationType);

//                if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
//                {
//                    throw new Exception("all ships are placed");
//                }

//                if (player.PlacedShips.Count > session.Settings.ShipCount)
//                {
//                    throw new Exception("enough ships are placed");
//                }

//                if (player.PlacedShips.Any(placedShip => placedShip.Type == ship.Type))
//                {
//                    throw new Exception("Such ship has already been placed");
//                }

//                _battleshipsFacade.PlaceShipToBoard(ship, board, cellCoordinates);
//                player.PlacedShips.Add(ship);

//                _battleshipHub.SendGameData(session);
//            }

//            if (isMine())
//            {
//                var factory = _battleshipsFacade.Factory;

//                var mine = factory.CreateMine(type, NationType.American);

//                var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
//                var player = session.GetPlayerByConnectionId(Context.ConnectionId);
//                var enemyPlayer = session.GetEnemyPlayerByConnectionId(Context.ConnectionId);
//                var enemyBoard = enemyPlayer.Board;

//                if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
//                {
//                    throw new Exception("all units are placed");
//                }

//                if (player.PlacedMines.Count > session.Settings.MineCount)
//                {
//                    throw new Exception("all mines are placed");
//                }

//                if (player.PlacedMines.Any(placedMine => placedMine.Type == mine.Type))
//                {
//                    throw new Exception("Such ship has already been placed");
//                }

//                _battleshipsFacade.PlaceMineToBoard(mine, enemyBoard, cellCoordinates);
//                player.PlacedMines.Add(mine);

//                SendGameData(session);
//            }

//        bool isHorizontalIDK() { return true; }
//        bool isShip() { return true; }
        
//        bool isMine() { return true; }

//    }
//}
