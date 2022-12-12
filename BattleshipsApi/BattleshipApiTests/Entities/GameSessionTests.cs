namespace BattleshipApiTests.Entities;

public class GameSessionTests
{
    private BattleshipsApi.Entities.GameSession _gameSession;
    private BattleshipsApi.Entities.Player _playerOne;
    private BattleshipsApi.Entities.Player _playerTwo;

    [SetUp]
    public void Setup()
    {
        _playerOne = new Player("playerOneConnectionId", "playerOne");
        _playerTwo = new Player("playerTwoConnectionId", "playerTwo");
        _gameSession = new GameSession(_playerOne, _playerTwo);
    }

    [Test]
    public void GetPlayerByConnectionId_ShouldReturnSamePlayer()
    { 
        // Act
        var result = _gameSession.GetPlayerByConnectionId(_playerOne.ConnectionId);

        // Assert
        result.Should().Be(_playerOne);
    }

    [Test]
    public void GetPlayerByConnectionId_NonExistingConnectionId_ShouldReturnNull()
    {
        // Act
        var result = _gameSession.GetPlayerByConnectionId("NotExists");

        Console.WriteLine(result);

        // Assert
        result.Should().Be(null);
    }

    [Test]
    public void SetMoveToNextPlayer_ShouldReturNextPlayerConnectionId()
    {
        // Act
        var playerOneMove = _gameSession.NextPlayerTurnConnectionId;
        _gameSession.SetMoveToNextPlayer();
        var playerTwoMove = _gameSession.NextPlayerTurnConnectionId;

        // Assert

        playerTwoMove.Should().NotBeSameAs(playerOneMove);
    }

    [Test]
    public void SwapPlayers_ShouldChangePlayersPositionInGameSession()
    {
        // Act
        var playerOne = _gameSession.PlayerOne;
        var playerTwo = _gameSession.PlayerTwo;
        _gameSession.SwapPlayers();


        // Assert
        _gameSession.PlayerOne.Should().Be(playerTwo);
        _gameSession.PlayerTwo.Should().Be(playerOne);
    }

    [Test]
    public void GetEnemyPlayerByConnectionId_ShouldReturnEnemyPlayer()
    {
        // Act
        var result = _gameSession.GetEnemyPlayerByConnectionId(_playerOne.ConnectionId);

        // Assert
        result.Should().Be(_playerTwo);
    }

    [Test]
    public void GetEnemyPlayerByConnectionId_NonExistingConnectionId_ShouldReturnEnemyPlayer()
    {
        // Act
        var result = _gameSession.GetEnemyPlayerByConnectionId("NonExisting");

        // Assert
        result.Should().Be(null);
    }
}