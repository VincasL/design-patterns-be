using System.Reflection;
using BattleshipsApi.Entities;
using BattleshipsApi.Handlers;
using FluentAssertions;

namespace BattleshipApiTests.Handlers;

public class QueueHandlerTests
{
    private QueueHandler _queueHandler;
    [SetUp]
    public void Setup()
    {
        _queueHandler = new QueueHandler();
    }

    [Test]
    public void AddPlayerToQueue_AddedToEmptyList_ShouldReturnFalse()
    {
        // Arrange
        var player = new Player("connId", "name");
        
        // Act
        var result = _queueHandler.AddPlayerToQueue(player);

        // Assert
        result.Should().BeFalse();
    }
    
    [Test]
    public void AddPlayerToQueue_AddedToListWithOneList_ShouldReturnTrue()
    {
        // Arrange
        var player = new Player("connId", "name");
        var player2 = new Player("connId2", "name");

        
        // Act
        _queueHandler.AddPlayerToQueue(player);
        var result = _queueHandler.AddPlayerToQueue(player2);


        // Assert
        result.Should().BeTrue();
    }
    
    [Test]
    public void AddPlayerToQueue_AddTwoPlayersWithSameConnectionid_ShouldReturnFalse()
    {
        // Arrange
        var player = new Player("connId", "name");
        var player2 = new Player("connId", "name");
        _queueHandler.AddPlayerToQueue(player);
        
        // Act

        var result = _queueHandler.AddPlayerToQueue(player2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Test]
    public void ReturnLastTwoPlayers_LessThanTwoPlayers_ShouldThrow()
    {
        // Arrange
        var player = new Player("connId", "name");
        _queueHandler.AddPlayerToQueue(player);
        // Act

        // Assert
        _queueHandler
            .Invoking(x => x.ReturnLastTwoPlayers())
            .Should()
            .Throw<Exception>();
    }
    
    [Test]
    public void ReturnLastTwoPlayers_TwoPlayers_ShouldReturnTwoPlayers()
    {
        // Arrange
        var player = new Player("connId", "name");
        var player2 = new Player("connId2", "name");

        _queueHandler.AddPlayerToQueue(player);
        _queueHandler.AddPlayerToQueue(player2);

        // Act
        var result = _queueHandler.ReturnLastTwoPlayers();

        // Assert
        result.Item1.Should().Be(player);
        result.Item2.Should().Be(player2);
    }
}