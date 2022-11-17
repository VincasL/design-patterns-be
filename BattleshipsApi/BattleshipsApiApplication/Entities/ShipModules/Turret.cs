using BattleshipsApi.Composite;

namespace BattleshipsApi.Entities.ShipModules;

public class Turret: IShipWeapon 
{
    public int Damage()
    {
        throw new NotImplementedException();
    }

    public int GetArmourStrength()
    {
        return 25;
    }

    public int GetFuel()
    {
        return 25;
    }
}