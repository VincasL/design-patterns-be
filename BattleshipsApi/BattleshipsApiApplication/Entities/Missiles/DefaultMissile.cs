using BattleshipsApi.Composite;

namespace BattleshipsApi.Entities.Missiles;

public class DefaultMissile : Missile, IShipComponent
{
    public override Unit Clone()
    {
        return new DefaultMissile();
    }

    public int GetArmourStrength()
    {
        return 5;
    }

    public int GetFuel()
    {
        return 0;
    }
}