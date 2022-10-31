using AutoMapper;

namespace BattleshipsApi.Entities;

public class GameSession: IGameSession
{
    private readonly IMapper _mapper;
    private Settings _defaultSettings = new Settings(10);

    public GameSession(Player playerOne, Player playerTwo, IMapper mapper, Settings? gameSettings = null)
    {
        _mapper = mapper;
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
        return _mapper.Map<GameSession>(this);
    }
}