using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Handlers;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Helpers;

public class GameDataAdapter : ITarget
{
    private GameDataSender _gameDataSender;

    public GameDataAdapter(IHubContext<BattleshipHub> context)
    {
        _gameDataSender = new GameDataSender(context);
    }

    public async Task SendGameData(GameSession session)
    {
        var gameData = new GameData();
        
        

        gameData.AllPlayersPlacedShips = session.AllPlayersPlacedShips;
        
        gameData.IsYourMove = session.NextPlayerTurnConnectionId == session.PlayerOne.ConnectionId;
        if (session.IsGameOver)
        {
            gameData.Winner = session.PlayerOne.Winner;
        }
        
        await _gameDataSender.SendGameData(new GameData(), session.PlayerOne.ConnectionId);
    }
}

public interface ITarget
{
    public Task SendGameData(GameSession session);

}