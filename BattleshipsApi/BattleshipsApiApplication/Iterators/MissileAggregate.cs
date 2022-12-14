using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class MissileAggregate : IAggregate
    {
        List<Unit> items = new List<Unit>();

        public MissileAggregate(List<Unit> items)
        {
            this.items = items;
        }

        public IIterator CreateIterator()
        {
            return new MissileIterator(this);
        }
        public int Count
        {
            get { return items.Count; }
        }
        // Indexer
        public Unit this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }
    }

}
