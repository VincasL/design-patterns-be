using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class MissileIterator : IIterator
    {
        List<Missile> aggregate;
        // Constructor
        public MissileIterator(List<Missile> aggregate)
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
            foreach (Missile ship in aggregate)
            {
                yield return ship;
            }
        }
    }
}
