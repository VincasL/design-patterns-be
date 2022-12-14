using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

public class MineBuilder : IBuilder
{
    private Mine _mine;
    public MineBuilder(Mine rawUnit) : base(rawUnit)
    {
        _mine = rawUnit;
    }

    public override IBuilder AddArmour(int armour)
    {
        _mine.ArmourStrength = armour;
        return this;
    }

    public override IBuilder AddDammage(int dammage)
    {
        _mine.Damage = dammage;
        return this;
    }
}
