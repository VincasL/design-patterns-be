using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Handlers;

namespace BattleshipsApi.Helpers;

public static class SessionHelpers
{
    public static GameSession CreateSession(Player playerOne, Player playerTwo)
    {
        var sessions = Sessions.GetSessions();
        var session = new GameSession(playerOne, playerTwo);
        sessions.Add(session);
        return session;
    }
    
    public static GameSession GetSessionByConnectionId(string connectionId)
    {
        var sessions = Sessions.GetSessions();
        var session = sessions.FirstOrDefault(
            x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId);

        return session ?? sessions.Last();
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