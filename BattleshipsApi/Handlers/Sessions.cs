using BattleshipsApi.Entities;

namespace BattleshipsApi.Handlers;

public class Sessions
{
    private static List<GameSession>? _sessions = null;
    private static object _threadLock = new object();
    
    private Sessions()
    {
    }

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

        return _sessions;
    }
}

