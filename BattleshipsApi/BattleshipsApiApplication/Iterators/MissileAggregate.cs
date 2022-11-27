using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class MissileAggregate : IAggregate
    {
        List<Missile> items = new List<Missile>();

        public MissileAggregate(List<Missile> items)
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
        public Missile this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }
    }

}
