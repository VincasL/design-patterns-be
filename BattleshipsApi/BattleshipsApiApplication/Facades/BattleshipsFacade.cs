using AutoMapper;
using BattleshipsApi.Entities;
using BattleshipsApi.Factories;
using BattleshipsApi.Handlers;
using BattleshipsApi.Helpers;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Facades;

public class BattleshipsFacade
{
    private readonly QueueHandler _queueHandler;
    private readonly GameLogicHandler _gameLogicHandler;
    private readonly GameDataAdapter _gameDataAdapter;
    private readonly GameDataSender _gameDataSender;
    public AbstractFactory Factory { get; }

    public BattleshipsFacade(QueueHandler queueHandler, GameLogicHandler gameLogicHandler, IMapper mapper, IHubContext<BattleshipHub> hubContext, GameDataSender gameDataSender)
    {
        _queueHandler = queueHandler;
        _gameLogicHandler = gameLogicHandler;
        _gameDataSender = gameDataSender;
        Factory = new AbstractFactory();
        _gameDataAdapter = new GameDataAdapter(hubContext, mapper);
    }

    public bool AddPlayerToQueue(Player player)
    {
        return _queueHandler.AddPlayerToQueue(player);
    }

    public Tuple<Player, Player> ReturnLastTwoPlayers()
    {
        return _queueHandler.ReturnLastTwoPlayers();
    }

    public void PlaceShipToBoard(Ship ship, Board board, CellCoordinates cellCoordinates)
    {
        _gameLogicHandler.PlaceShipToBoard(ship, board, cellCoordinates);
    }
    
    public void PlaceMineToBoard(Mine mine, Board board, CellCoordinates cellCoordinates)
    {
        _gameLogicHandler.PlaceMineToBoard(mine, board, cellCoordinates);
    }

    public Unit? GetUnitByCellCoordinates(CellCoordinates cellCoordinates, Board board, Type? unitType = null)
    {
        return _gameLogicHandler.GetUnitByCellCoordinates(cellCoordinates, board, unitType);
    }

    public void UndoPlaceShipToBoardByCell(Ship ship, Board board)
    {
        _gameLogicHandler.UndoPlaceShipToBoardByCell(ship, board);
    }

    public (bool hasShipBeenHit, bool hasShipBeenDestroyed) MakeMoveToEnemyBoard(CellCoordinates cellCoordinates, Board board)
    {
        return _gameLogicHandler.MakeMoveToEnemyBoard(cellCoordinates, board);
    }

    public async Task SendGameData(GameSession session)
    {
        await _gameDataAdapter.SendGameData(session);
    }
    
    public async Task StartGame(GameSession session)
    {
        await _gameDataSender.SendStartGame(session.PlayerOne.ConnectionId, session.PlayerTwo.ConnectionId);
    }

    public GameSession GetSessionByConnectionId(string contextConnectionId)
    {
        return Sessions.GetSessionByConnectionId(contextConnectionId);
    }

    public GameSession CreateSession(Player player1, Player player2)
    {
        return Sessions.CreateSession(player1, player2);
    }

    public void BindNewConnectionIdToPlayer(string connectionId, string contextConnectionId, GameSession session)
    {
        Sessions.BindNewConnectionIdToPlayer(connectionId, contextConnectionId, session);
    }
    
    public int ExplodeMinesInCellsIfThereAreShips(Board board)
    {
        return _gameLogicHandler.ExplodeMinesInCellsIfThereAreShips(board);
    }
}