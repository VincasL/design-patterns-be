using AutoMapper;
using BattleshipsApi.Helpers;

namespace BattleshipsApi.Entities;

public class GameSession : IPrototype
{
    private Settings _defaultSettings = new Settings(10);

    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }
    public string NextPlayerTurnConnectionId { get; set; }
    public bool IsGameOver { get; set; }
    public Settings Settings { get; set; }

    public bool AllPlayersPlacedUnits =>
        PlayerOne.AreAllUnitsPlaced && PlayerTwo.AreAllUnitsPlaced;

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

    public GameSession ShowPlayerTwoMines()
    {
        PlayerTwo.Board.RevealBoardMines();
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

    public object Clone()
    {
        var newGameSession = new GameSession(
            new Player(PlayerOne.ConnectionId, PlayerOne.Name, (Board)PlayerOne.Board.Clone(), PlayerOne.AreAllUnitsPlaced,
                 PlayerOne.Winner, PlayerOne.PlacedShips, PlayerOne.PlacedMines),
            new Player(PlayerTwo.ConnectionId, PlayerTwo.Name, (Board)PlayerTwo.Board.Clone(), PlayerTwo.AreAllUnitsPlaced,
                 PlayerTwo.Winner, PlayerTwo.PlacedShips, PlayerTwo.PlacedMines),
            NextPlayerTurnConnectionId,
            IsGameOver,
            Settings
        );
        return newGameSession;
    }

    public object ShallowClone()
    {
        return this.MemberwiseClone();
    }
}