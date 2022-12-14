using BattleshipsApi.Contracts;

namespace BattleshipsApi.Iterators
{
    public class MissileIterator : IIterator
    {
        MissileAggregate aggregate;
        // Constructor
        public MissileIterator(MissileAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        // Gets first iteration item
        public object First()
        {
            for (int i = 0; i < aggregate.Count; i++)
            {
                if (aggregate[i] is Missile)
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
                if (aggregate[i] is Missile)
                {
                    yield return aggregate[i];
                }
            }
        }
    }
}
