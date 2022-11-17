using BattleshipsApi.Composite;

namespace BattleshipsApi.Entities.ShipModules;

public class Cannon: IShipWeapon
{
    public int GetArmourStrength()
    {
        return 5;
    }

    public int GetFuel()
    {
        return 10;
    }

    public int Damage()
    {
        return 25;
    }
}