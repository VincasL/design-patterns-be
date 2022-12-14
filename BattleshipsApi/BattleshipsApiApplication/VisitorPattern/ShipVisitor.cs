using BattleshipsApi.Entities;
using BattleshipsApi.Entities.Ships;
using BattleshipsApi.Facades;
using System.Threading;

namespace BattleshipsApi.VisitorPattern
{
    public class ShipVisitor : IVisitor
    {
        DateTime timeGameStarted = new DateTime();
        double timeNow = 0;
        public ShipVisitor(DateTime time) {
            timeGameStarted = time;
            timeNow = (DateTime.UtcNow - timeGameStarted).TotalSeconds;
        }
        public int Visit(Carrier carrier)
        {
            if (carrier != null)
            {
                if (timeNow > 0 && timeNow < 60)
                {
                    return carrier.Speed = 3;
                }
                if (timeNow >= 60 && timeNow < 120)
                {
                    return carrier.Speed = 2;
                }
                if(timeNow >= 120 && timeNow < 300)
                {
                    return carrier.Speed = 1;
                }
            }
            return 0;
        }

        public int Visit(Battleship battleship)
        {
            if (battleship != null)
            {
                if (timeNow > 0 && timeNow < 30)
                {
                    return battleship.Speed = 10;
                }
                if (timeNow >= 30 && timeNow < 300)
                {
                    return battleship.Speed = 2;
                }
                if (timeNow >= 300)
                {
                    return battleship.Speed = 1;
                }
            }
            return 0;
        }

        public int Visit(Cruiser cruiser)
        {
            if (cruiser != null)
            {
                if (timeNow > 0 && timeNow < 60)
                {
                    return cruiser.Speed = 4;
                }
                if (timeNow >= 60 && timeNow < 300)
                {
                    return cruiser.Speed = 3;
                }
                if (timeNow >= 300)
                {
                    return cruiser.Speed = 1;
                }
            }
            return 0;
        }

        public int Visit(Destroyer destroyer)
        {
            if (destroyer != null)
            {
                if (timeNow > 0 && timeNow < 60)
                {
                    return destroyer.Speed = 5;
                }
                if (timeNow >= 60 && timeNow < 120)
                {
                    return destroyer.Speed = 4;
                }
                if (timeNow >= 120 && timeNow < 300)
                {
                    return destroyer.Speed = 3;
                }
                if (timeNow >= 300)
                {
                    return destroyer.Speed = 2;
                }
            }
            return 0;
        }

        public int Visit(Submarine submarine)
        {
            if (submarine != null)
            {
                if (timeNow > 0 && timeNow < 60)
                {
                    return submarine.Speed = 4;
                }
                if (timeNow >= 60 && timeNow < 300)
                {
                    return submarine.Speed = 3;
                }
                if (timeNow >= 300)
                {
                    return submarine.Speed = 1;
                }
            }
            return 0;
        }
    }
}
