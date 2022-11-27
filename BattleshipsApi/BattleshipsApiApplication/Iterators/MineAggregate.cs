using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class MineAggregate : IAggregate
    {
        List<Mine> items = new List<Mine>();

        public MineAggregate(List<Mine> items)
        {
            this.items = items;
        }

        public IIterator CreateIterator()
        {
            return new MineIterator(this);
        }
        public int Count
        {
            get { return items.Count; }
        }
        // Indexer
        public Mine this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }
    }

}
