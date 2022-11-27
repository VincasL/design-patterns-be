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
            return aggregate[0];
        }
        // Gets next iteration item
        public IEnumerable<object> GetEnumerator()
        {
            for (int i=0;i<aggregate.Count;i++)
            {
                yield return aggregate[i];
            }
        }
    }
}
