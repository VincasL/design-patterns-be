using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.Iterators
{
    public class MineIterator : IIterator
    {
        List<Mine> aggregate;
        // Constructor
        public MineIterator(List<Mine> aggregate)
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
            foreach (Mine ship in aggregate)
            {
                yield return ship;
            }
        }
    }
}
