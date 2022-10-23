using BattleshipsApi.Entities;

namespace BattleshipsApi.Handlers;

public static class Sessions
{
    private static List<GameSession>? _sessions = null;
    private static object _threadLock = new object();

    public static List<GameSession> GetSessions()
    {
        lock (_threadLock)
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

