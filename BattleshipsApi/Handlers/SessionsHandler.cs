using BattleshipsApi.Entities;
using BattleshipsApi.Models;

namespace BattleshipsApi.Handlers;

public class SessionsHandler
{
    private List<GameSession> _sessions = new();
    
    public SessionsHandler()
    {
    }

    public GameSession CreateSession(GamePlayer playerOne, GamePlayer playerTwo)
    {
        var session = new GameSession(playerOne, playerTwo);
        _sessions.Add(session);
        return session;
    }
    
    public GameSession GetSessionByConnectionId(string connectionId)
    {
        return _sessions.First(
            x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId);
    }
}

