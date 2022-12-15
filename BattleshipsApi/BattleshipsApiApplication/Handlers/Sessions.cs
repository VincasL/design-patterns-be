using BattleshipsApi.Entities;

namespace BattleshipsApi.Handlers;

public static class Sessions
{
    private static List<GameSession>? _sessions;
    private static readonly object ThreadLock = new();

    private static List<GameSession> GetSessions()
    {
        lock (ThreadLock)
        {
            if (_sessions == null)
            {
                Console.WriteLine("Sessions initialized");
                _sessions = new List<GameSession>();
            }
        }
        Console.WriteLine("\n\nSessions returned\n");
        return _sessions;
    }
    
    public static GameSession CreateSession(Player playerOne, Player playerTwo)
    {
        var sessions = GetSessions();
        var session = new GameSession(playerOne, playerTwo);
        sessions.Add(session);
        return session;
    }
    
    public static GameSession GetSessionByConnectionId(string connectionId)
    {
        var sessions = GetSessions();
        var session = sessions.FirstOrDefault(
            x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId);

        if (session == null)
        {
            throw new Exception("No connection found with this connectionId! Try restarting the game");
        }
        return session;
    }

    public static void BindNewConnectionIdToPlayer(string oldConnectionId, string newConnectionId, GameSession session)
    {
        session.GetPlayerByConnectionId(oldConnectionId).ConnectionId = newConnectionId;
        if (session.NextPlayerTurnConnectionId == oldConnectionId)
        {
            session.NextPlayerTurnConnectionId = newConnectionId;
        }
    }
}
