using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class ShipAggregate : IAggregate
    {
        List<Ship> items = new List<Ship>();

        public ShipAggregate(List<Ship> items)
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
            get { return items[index]; }
            set { items.Add(value); }
        }
    }
}
