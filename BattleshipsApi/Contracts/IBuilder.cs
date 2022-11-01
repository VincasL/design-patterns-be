using BattleshipsApi.Entities;

namespace BattleshipsApi.Contracts
{
    public abstract class IBuilder
    {
        protected Unit rawUnit;
        public IBuilder(Unit rawUnit)
        {
            this.rawUnit = rawUnit;
        }
        public abstract IBuilder AddArmour(int armour);
        public abstract IBuilder AddDammage(int dammage);
        public IBuilder AddStrategy(MoveStrategy strategy)
        {
            rawUnit.MoveStrategy = strategy;
            return this;
        }
        public Unit Build()
        {
            return rawUnit;
        }
    }
}
