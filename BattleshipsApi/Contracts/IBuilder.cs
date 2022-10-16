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
        public abstract IBuilder AddParts();
        public abstract IBuilder AssemblyBody();
        public Unit build()
        {
            return null;
        }
        public IBuilder AddAlgorithm(MoveStrategy algo)
        {
            return null;
        }
    }
}
