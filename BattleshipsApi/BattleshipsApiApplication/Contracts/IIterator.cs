namespace BattleshipsApi.Contracts
{
    public interface IIterator
    {
        public abstract object First();
        public abstract IEnumerable<object> GetEnumerator();
    }
}
