using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class ShipIterator : IIterator
    {
        List<Ship> aggregate;
        // Constructor
        public ShipIterator(List<Ship> aggregate)
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
            foreach (Ship ship in aggregate)
            {
                yield return ship;
            }
        }
    }
}
