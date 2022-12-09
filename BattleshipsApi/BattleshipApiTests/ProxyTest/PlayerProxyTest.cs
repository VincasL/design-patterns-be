using BattleshipsApi.Factories;
using BattleshipsApi.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApiTests.ProxyTest
{
    public class PlayerProxyTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void PlayerProxyTest_returnShips()
        {
            //Arrange
            Player player = new Player("connectionId", "Stepas");

            var factory = new AbstractFactory();
            var ship = factory.CreateShip(ShipType.Cruiser, NationType.American);

            player.PlacedShips.Add(ship);

            IGetPlayerData playerProxy = new PlayerProxy();

            foreach (var item in playerProxy.GetPlayerShips("connectionId", "Stepas"))
            {
                Console.WriteLine("\n Ship: " + item.Type + " " + item.Length);
            }


            //Act & Assert
        }
    }
}
