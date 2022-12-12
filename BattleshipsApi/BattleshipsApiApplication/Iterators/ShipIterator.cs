using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;
using BattleshipsApi.States.ShipStates;
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
        //Gets first iteration item
        public object First()
        {
            for (int i = 0; i < aggregate.Count; i++)
            {
                if (aggregate[i] is Ship && !(aggregate[i].ShipState is ShipDestroyed))
                {
                    return aggregate[i];
                }
            }
            return null;
        }

        public IEnumerable<object> GetEnumerator()
        {
            for (int i = 0; i < aggregate.Count; i++)
            {
                if (aggregate[i] is Ship && !(aggregate[i].ShipState is ShipDestroyed))
                {
                    yield return aggregate[i];
                }
            }
        }

    }
}
