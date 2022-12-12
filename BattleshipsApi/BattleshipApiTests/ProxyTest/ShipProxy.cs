using BattleshipsApi.Proxy;

namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class ShipProxy
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
        IGetShipData shipProxy = new BattleshipsApi.Proxy.ShipProxy();

        Console.WriteLine("\n Carrier length: " + shipProxy.GetShipSize(ShipType.Carrier, NationType.American));
        Console.WriteLine("\n Battleship length: " + new BattleshipsApi.Proxy.ShipProxy().GetShipSize(ShipType.Battleship, NationType.American));
        Console.WriteLine("\n Cruiser length: " + new BattleshipsApi.Proxy.ShipProxy().GetShipSize(ShipType.Cruiser, NationType.American));
        Console.WriteLine("\n Submarine length: " + new BattleshipsApi.Proxy.ShipProxy().GetShipSize(ShipType.Submarine, NationType.American));
        Console.WriteLine("\n Destroyer length: " + new BattleshipsApi.Proxy.ShipProxy().GetShipSize(ShipType.Destroyer, NationType.American));

        //Act & Assert
        Assert.AreEqual(shipsize, shipProxy.GetShipSize(ShipType.Carrier, NationType.American));
    }

}