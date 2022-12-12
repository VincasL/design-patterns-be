using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class ShipAggregate : IAggregate
    {
        List<Unit> items = new List<Unit>();

        public ShipAggregate(List<Unit> items)
        {
            this.items = items;
        }

        public IIterator CreateIterator()
        {
            return new ShipIterator(this);
        }
        public int Count
        {
            get { return items.Count; }
        }
        // Indexer
        public Ship this[int index]
        {
            get { return (Ship)items[index]; }
            set { items.Add(value); }
        }
    }
}
