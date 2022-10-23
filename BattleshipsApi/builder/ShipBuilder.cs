using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using System;

public class ShipBuilder : IBuilder
{
    public ShipBuilder(Unit rawUnit) : base(rawUnit)
    {
        
    }

    public override IBuilder AddParts()
    {
        throw new NotImplementedException();
    }

    public override IBuilder AssemblyBody()
    {
        throw new NotImplementedException();
    }
}
