﻿namespace BattleshipsApi.Entities;

public class GameSession
{
    private Settings _defaultSettings = new Settings(10);

    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }
    public string NextPlayerTurnConnectionId { get; set; }
    public bool IsGameOver { get; set; } = false;
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
    

    public Player GetPlayerByConnectionId(string connectionId)
    {
        return PlayerOne.ConnectionId == connectionId ? PlayerOne : PlayerTwo;
    }
    
    public Player GetEnemyPlayerByConnectionId(string connectionId)
    {
        return PlayerOne.ConnectionId != connectionId ? PlayerOne : PlayerTwo;
    }



    public void SetMoveToNextPlayer() => 
        NextPlayerTurnConnectionId = NextPlayerTurnConnectionId == PlayerOne.ConnectionId ? PlayerTwo.ConnectionId : PlayerOne.ConnectionId;
}