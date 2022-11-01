using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using System;

public class ShipBuilder : IBuilder
{
    private Ship _ship;

    public ShipBuilder(Ship rawUnit) : base(rawUnit)
    {
        _ship = rawUnit;
    }

    public override IBuilder AddArmour(int armour)
    {
        _ship.ArmourStrength = armour;
        return this;
    }

    public override IBuilder AddDammage(int dammage)
    {
        return this;
    }
}
