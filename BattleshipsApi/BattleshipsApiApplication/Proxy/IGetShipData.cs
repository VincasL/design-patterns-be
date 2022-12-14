using BattleshipsApi.Entities;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Proxy
{
    public interface IGetShipData
    {
        int GetShipSize(ShipType type, NationType nationType);
    }
}
