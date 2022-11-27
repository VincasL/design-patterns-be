using AutoMapper;
using BattleshipsApi;
using BattleshipsApi.DTO;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace BattleshipApiTests.Handlers.GameDataSender;

public class GameDataSender_Tests
{
    private static string connectionId = "connectionId";
    private static Player player1 = new Player("connectionId", "Jonas");
    private static Player player2 = new Player("connectionId", "Antanas");
    private static GameSession session = BattleshipsApi.Handlers.Sessions.CreateSession(player1, player2);

    private BattleshipsApi.Handlers.GameDataSender _gameDataSender;

    [Test]
    public void SendGameDataTest()
    {
        //Arrange
        var clients = new Mock<IHubClients>();
        var context = new Mock<IHubContext<BattleshipHub>>();
        context.Setup(m => m.Clients).Returns(clients.Object);
        var mapper = new Mock<IMapper>();
        _gameDataSender = new(context.Object);

        var myProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper __mapper = new Mapper(configuration);

        var gameData = __mapper.Map<GameData>(session);
        _gameDataSender.SendGameData(gameData, connectionId);
    }
    [Test]
    public void SendStartGameTest()
    {
        //Arrange
        var clients = new Mock<IHubClients>();
        var context = new Mock<IHubContext<BattleshipHub>>();
        context.Setup(m => m.Clients).Returns(clients.Object);
        _gameDataSender = new(context.Object);

        var myProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper __mapper = new Mapper(configuration);


        var gameData = __mapper.Map<GameData>(session);
        _gameDataSender.SendStartGame(player1.ConnectionId, player2.ConnectionId);
    }

}