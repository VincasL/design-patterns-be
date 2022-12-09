using BattleshipsApi.Entities.Ships;

namespace BattleshipsApi.VisitorPattern
{
    public class ShipVisitor : IVisitor
    {
        public int Visit(Carrier carrier)
        {
            if (carrier != null)
            {
                return carrier.Speed = (int)Math.Floor(carrier.Length * 0.2);
            }
            return int.MinValue;
        }

        public int Visit(Battleship battleship)
        {
            if (battleship != null)
            {
                return battleship.Speed = (int)Math.Floor(battleship.Length * 0.5);
            }
            return int.MinValue;
        }

        public int Visit(Cruiser cruiser)
        {
            if (cruiser != null)
            {
                return cruiser.Speed = cruiser.Length;
            }
            return int.MinValue;
        }

        public int Visit(Submarine submarine)
        {
            if (submarine != null)
            {
                return submarine.Speed = submarine.Length;
            }
            return int.MinValue;
        }

        public int Visit(Destroyer destroyer)
        {
            if (destroyer != null)
            {
                return destroyer.Speed = (int)Math.Floor((double)destroyer.Length * 2);
            }
            return int.MinValue;
        }
    }
}
