using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Factories;
using BattleshipsApi.Proxy;

namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class ProxyTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ShipProxyTest()
    {
        //Arrange
        int shipsize = 5;
        IGetShipData shipProxy = new ShipProxy();

        Console.WriteLine("\n Carrier length: " + shipProxy.GetShipSize(ShipType.Carrier, NationType.American));
        Console.WriteLine("\n Battleship length: " + new ShipProxy().GetShipSize(ShipType.Battleship, NationType.American));
        Console.WriteLine("\n Cruiser length: " + new ShipProxy().GetShipSize(ShipType.Cruiser, NationType.American));
        Console.WriteLine("\n Submarine length: " + new ShipProxy().GetShipSize(ShipType.Submarine, NationType.American));
        Console.WriteLine("\n Destroyer length: " + new ShipProxy().GetShipSize(ShipType.Destroyer, NationType.American));

        //Act & Assert
        Assert.AreEqual(shipsize, shipProxy.GetShipSize(ShipType.Carrier, NationType.American));
    }

}