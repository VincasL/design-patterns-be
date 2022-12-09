using BattleshipsApi.Entities;

namespace BattleshipsApi.Proxy
{
    public interface IGetPlayerData
    {
        public List<Ship> GetPlayerShips(string connection, string name);
    }
}
