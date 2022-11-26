namespace BattleshipsApi.Contracts
{
    public abstract class IAggregate
    {
        public abstract IIterator CreateIterator<T>();
    }
}
