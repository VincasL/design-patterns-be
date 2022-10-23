using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using System;

public class MissileBuilder : IBuilder
{
    public MissileBuilder(Unit rawUnit) : base(rawUnit)
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
