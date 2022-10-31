using AutoMapper;
using BattleshipsApi.Helpers;

namespace BattleshipsApi.Entities;

public class GameSession
{
    private Settings _defaultSettings = new Settings(10);
    
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }
    public string NextPlayerTurnConnectionId { get; set; }
    public bool IsGameOver { get; set; }
    public Settings Settings { get; set; }
    
    public bool AllPlayersPlacedShips =>
        PlayerOne.AreAllShipsPlaced && PlayerTwo.AreAllShipsPlaced;

    public GameSession(Player playerOne, Player playerTwo, Settings? gameSettings = null)
    {
        Settings = gameSettings ?? _defaultSettings;
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        playerOne.Board = new Board(Settings.BoardSize);
        playerTwo.Board = new Board(Settings.BoardSize);
        NextPlayerTurnConnectionId = PlayerOne.ConnectionId;
    }

    private GameSession(Player playerOne, Player playerTwo, string nextPlayerTurnConnectionId, bool isGameOver, Settings settings)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        NextPlayerTurnConnectionId = nextPlayerTurnConnectionId;
        IsGameOver = isGameOver;
        Settings = settings;
    }
    
    public Player GetPlayerByConnectionId(string connectionId)
    {
        return PlayerOne.ConnectionId == connectionId ? PlayerOne : PlayerTwo;
    }
    
    public Player GetEnemyPlayerByConnectionId(string connectionId)
    {
        return PlayerOne.ConnectionId != connectionId ? PlayerOne : PlayerTwo;
    }

    public GameSession ShowPlayerOneShips()
    {
        PlayerOne.Board.RevealBoardShips();
        return this;
    }

    public GameSession SwapPlayers()
    {
        (PlayerOne, PlayerTwo) = (PlayerTwo, PlayerOne);
        return this;
    }



    public GameSession SetMoveToNextPlayer()
    {
        NextPlayerTurnConnectionId = NextPlayerTurnConnectionId == PlayerOne.ConnectionId ? PlayerTwo.ConnectionId : PlayerOne.ConnectionId;
        return this;
    }

    public GameSession Clone()
    {
        var newGameSession = new GameSession(
            new Player(PlayerOne.ConnectionId, PlayerOne.Name, PlayerOne.Board.Clone(), PlayerOne.AreAllShipsPlaced,
                PlayerOne.DestroyedShipCount, PlayerOne.Winner),
            new Player(PlayerTwo.ConnectionId, PlayerTwo.Name, PlayerTwo.Board.Clone(), PlayerTwo.AreAllShipsPlaced,
                PlayerTwo.DestroyedShipCount, PlayerTwo.Winner),
            NextPlayerTurnConnectionId,
            IsGameOver,
            Settings
        );
        return newGameSession;
    }
}