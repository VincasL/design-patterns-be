namespace BattleshipsApi.Contracts
{
    public interface IAggregate
    {
        public abstract IIterator CreateIterator();
    }
}
