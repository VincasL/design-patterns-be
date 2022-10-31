using BattleshipsApi.Entities;

namespace BattleshipsApi.Handlers;

public static class Sessions
{
    private static List<GameSession>? _sessions;
    private static readonly object ThreadLock = new();

    public static List<GameSession> GetSessions()
    {
        lock (ThreadLock)
        {
            if (_sessions == null)
            {
                Console.WriteLine("Sessions initialized");
                _sessions = new List<GameSession>();
            }
        }

        Console.WriteLine("Sessions returned");
        return _sessions;
    }
}

