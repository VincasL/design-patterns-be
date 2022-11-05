using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

public class MissileBuilder : IBuilder
{
    private Missile _missile;
    public MissileBuilder(Missile rawUnit) : base(rawUnit)
    {
        _missile = rawUnit;
    }

    public override IBuilder AddArmour(int armour)
    {
        return this;
    }

    public override IBuilder AddDammage(int dammage)
    {
        _missile.Dammage = dammage;
        return this;
    }
}
