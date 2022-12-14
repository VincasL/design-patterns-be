using BattleshipsApi.Entities.Ships;

namespace BattleshipsApi.VisitorPattern
{
    public interface IVisitor
    {
        public int Visit(Carrier carrier);
        public int Visit(Battleship battleship);
        public int Visit(Cruiser cruiser);
        public int Visit(Submarine submarine);
        public int Visit(Destroyer destroyer);
    }
}
