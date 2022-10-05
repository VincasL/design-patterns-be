using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class GameSession
{
    private GameSettings _defaultSettings = new GameSettings(10);
    
    public GamePlayer PlayerOne { get; set; }
    public GamePlayer PlayerTwo { get; set; }
    public bool AreShipsPlaced { get; set; }
    public PlayerTurn PlayerTurn { get; set; }
    public GameSettings Settings { get; set; }
    public Board Board { get; set; }

    public GameSession(GamePlayer playerOne, GamePlayer playerTwo, GameSettings? gameSettings = null)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        Settings = gameSettings ?? _defaultSettings;
        Board = CreateBoard(Settings.BoardSize);
    }

    private static Board CreateBoard(int settingsBoardSize)
    {
        var cells = new Cell[settingsBoardSize, settingsBoardSize];
        var board = new Board();
        board.Cells = cells;
        return board;
    }

    public GamePlayer GetPlayerByConnectionId(string connectionId)
    {
        return PlayerOne.ConnectionId == connectionId ? PlayerOne : PlayerTwo;
    }
    
    public bool AllPlayersPlacedShips =>
        PlayerOne.Ships.Count > 0 && PlayerTwo.Ships.Count > 0;

    public void SetMoveToNextPlayer() => 
        PlayerTurn = PlayerTurn == PlayerTurn.FirstPlayer ? PlayerTurn.SecondPlayer : PlayerTurn.FirstPlayer;
}