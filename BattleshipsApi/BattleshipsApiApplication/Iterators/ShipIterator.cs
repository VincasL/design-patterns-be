using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using System.Collections;

namespace BattleshipsApi.Iterators
{
    public class ShipIterator : IIterator
    {
        ShipAggregate aggregate;
        // Constructor
        public ShipIterator(ShipAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        // Gets first iteration item
        public object First()
        {
            return aggregate[0];
        }

        public IEnumerable<object> GetEnumerator()
        {
            for (int i = 0; i < aggregate.Count; i++)
            {
                yield return aggregate[i];
            }
        }

    }
}
