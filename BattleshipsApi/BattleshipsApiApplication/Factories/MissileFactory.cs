using BattleshipsApi.Entities;
using BattleshipsApi.Entities.Missiles;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Factories;

public class MissileFactory
{
    public Missile GetMissile(MissileType type)
    {
        switch (type)
        {
            case MissileType.Default:
                return new DefaultMissile();
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}