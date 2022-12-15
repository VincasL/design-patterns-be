using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class MineIterator : IIterator
    {
        MineAggregate aggregate;
        // Constructor
        public MineIterator(MineAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        // Gets first iteration item
        public object First()
        {
            for (int i = 0; i < aggregate.Count; i++)
            {
                if (aggregate[i] is Mine)
                {
                    return aggregate[i];
                }
            }
            return null;
        }
        // Gets next iteration item
        public IEnumerable<object> GetEnumerator()
        {
            for (int i = 0; i < aggregate.Count; i++)
            {
                if (aggregate[i] is Mine)
                {
                    yield return aggregate[i];
                }
            }
        }
    }
}
