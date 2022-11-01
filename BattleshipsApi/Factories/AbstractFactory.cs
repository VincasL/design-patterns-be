using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Factories
{
    public class AbstractFactory
    {
        public Ship CreateShip(ShipType type, string nation)
        {
            ShipFactory _shipFactory = new ShipFactory();
            Ship ship=_shipFactory.GetShip(type);
            Unit unit = SetNation(new ShipBuilder(ship), nation);
            return unit as Ship;

        }
        public Mine CreateMine(MineType type, string nation)
        {
            MineFactory _mineFactory = new MineFactory();
            Mine mine = _mineFactory.GetMine(type);
            Unit unit = SetNation(new MineBuilder(mine), nation);
            return unit as Mine;
        }
        public Missile CreateMissile(MissileType type, string nation)
        {
            MissileFactory _missileFactory = new MissileFactory();
            Missile missile = _missileFactory.GetMissile(type);
            Unit unit = SetNation(new MissileBuilder(missile), nation);
            return unit as Missile;
        }
        private Unit SetNation(IBuilder builder, string nation)
        {
            Unit unit = new();
            Director _director = new Director();
            if (nation == "American")
            {
                unit = _director.ConstructAmerican(builder);
            }
            else if (nation == "Russian")
            {
                unit = _director.ConstructRussian(builder);
            }
            else if (nation == "German")
            {
                unit = _director.ConstructGerman(builder);
            }
            return unit;
        }
    }
}
