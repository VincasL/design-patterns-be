using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Handlers;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Helpers;

public class GameDataAdapter : ITarget
{
    private readonly GameDataSender _gameDataSender;
    private readonly IMapper _mapper;

    public GameDataAdapter(IHubContext<BattleshipHub> context, IMapper mapper)
    {
        _mapper = mapper;
        _gameDataSender = new GameDataSender(context);
    }

    public async Task SendGameData(GameSession session)
    {
        var gameData = _mapper.Map<GameData>(session);
        gameData.IsYourMove = session.NextPlayerTurnConnectionId == session.PlayerOne.ConnectionId;
        if (session.IsGameOver)
        {
            gameData.Winner = session.PlayerOne.Winner;
        }

        gameData.PlayerOne.AreAllShipsPlaced = session.PlayerOne.AreAllUnitsPlaced;
        gameData.AllPlayersPlacedShips = session.AllPlayersPlacedUnits;

        var boardSize = session.Settings.BoardSize;
        var playerOneBoardCells = new CellDto[session.Settings.BoardSize][];
        var playerTwoBoardCells = new CellDto[session.Settings.BoardSize][];
        
        for(var i = 0; i < boardSize; i++)
        {
            playerOneBoardCells[i] = new CellDto[boardSize];
            playerTwoBoardCells[i] = new CellDto[boardSize];
        }

        foreach (var cell in session.PlayerOne.Board.Cells)
        {
            playerOneBoardCells[cell.X][cell.Y] = _mapper.Map<CellDto>(cell);
        }

        foreach (var cell in session.PlayerTwo.Board.Cells)
        {
            playerTwoBoardCells[cell.X][cell.Y] = _mapper.Map<CellDto>(cell);
        }

        gameData.PlayerOne.Board.Cells = playerOneBoardCells;
        gameData.PlayerTwo.Board.Cells = playerTwoBoardCells;
        
        await _gameDataSender.SendGameData(gameData, session.PlayerOne.ConnectionId);
    }
}

public interface ITarget
{
    public Task SendGameData(GameSession session);
}