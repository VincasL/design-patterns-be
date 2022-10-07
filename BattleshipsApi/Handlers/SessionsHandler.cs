using BattleshipsApi.Entities;

namespace BattleshipsApi.Handlers;

public class SessionsHandler
{
    private List<Session> _sessions = new();
    
    public SessionsHandler()
    {
    }

    public Session CreateSession(Player playerOne, Player playerTwo)
    {
        var session = new Session(playerOne, playerTwo);
        _sessions.Add(session);
        return session;
    }
    
    public Session GetSessionByConnectionId(string connectionId)
    {
        return _sessions.First(
            x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId);
    }
}

