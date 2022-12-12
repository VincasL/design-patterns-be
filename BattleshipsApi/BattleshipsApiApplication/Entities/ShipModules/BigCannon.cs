namespace BattleshipsApi.Entities.ShipModules;

public class BigCannon: IShipWeapon
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