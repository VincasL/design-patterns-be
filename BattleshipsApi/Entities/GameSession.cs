namespace BattleshipsApi.Entities;

public class GameSession: IGameSession
{
    private Settings _defaultSettings = new Settings(10);

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