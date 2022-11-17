using BattleshipsApi.Composite;

namespace BattleshipsApi.Entities.ShipModules;

public class RocketLauncher: IShipWeapon
{
    public int GetArmourStrength()
    {
        return 5;
    }

    public int GetFuel()
    {
        return 2;
    }

    public int Damage()
    {
        return 25;
    }
}