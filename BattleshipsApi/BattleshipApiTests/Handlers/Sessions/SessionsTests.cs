namespace BattleshipApi.Handlers.Sessions;

public class SessionsTests
{
    private static string connectionId = "connectionId";
    private static Player player1 = new Player("connectionId", "Jonas");
    private static Player player2 = new Player("connectionId", "Antanas");
    private static GameSession session = BattleshipsApi.Handlers.Sessions.CreateSession(player1, player2);
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateSessionTest()
    {
        // Arrange
        var player1 = new Player("connectionId", "Jonas");
        var player2 = new Player("connectionId", "Antanas");

        // Act & Assert
        Assert.DoesNotThrow(() => BattleshipsApi.Handlers.Sessions.CreateSession(player1, player2));
    }

    [Test]
    public void GetSessionByConnectionIdNotExistingTest()
    {
        // Arrange & Act & Assert
        Assert.Throws<Exception>(() => BattleshipsApi.Handlers.Sessions.GetSessionByConnectionId("notExistingConnectionId"));
        
    }

    [Test]
    public void GetSessionByConnectionIdTest()
    {
        // Arrange & Act & Assert
        Assert.DoesNotThrow(() => BattleshipsApi.Handlers.Sessions.GetSessionByConnectionId(connectionId));
    }

    [Test]
    public void BindNewConnectionIdToPlayerTest()
    {
        // Arrange & Act & Assert
        Assert.DoesNotThrow(() => BattleshipsApi.Handlers.Sessions.BindNewConnectionIdToPlayer(connectionId, "newConnectionId", session));
    }
}