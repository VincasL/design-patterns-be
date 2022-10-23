using BattleshipsApi.Entities;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Factories
{
    public class AbstractFactory
    {
        public Ship CreateShip(ShipType type)
        {
            ShipFactory _shipFactory = new ShipFactory();
            return _shipFactory.GetShip(type);

        }
        public Mine CreateMine(MineType type)
        {
            MineFactory _mineFactory = new MineFactory();
            return _mineFactory.GetMine(type);
        }
        public Missile CreateMissile(MissileType type)
        {
            MissileFactory _missileFactory = new MissileFactory();
            return _missileFactory.GetMissile(type);
        }
    }
}
