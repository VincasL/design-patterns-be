namespace BattleshipsApi.Entities.ShipModules;

public class RocketLauncher: IShipWeapon
{
    public int GetArmourStrength()
    {
        return 5;
    }

    public int Damage()
    {
        return 25;
    }
}