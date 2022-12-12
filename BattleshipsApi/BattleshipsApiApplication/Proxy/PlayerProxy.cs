using BattleshipsApi.Entities;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Proxy
{
    public class PlayerProxy : IGetPlayerData
    {
        private Player player = null;
        public List<Ship> GetPlayerShips(string connection, string name)
        {
            if (player == null)
            {
                Player player = new(connection, name);
            }

            return player.GetPlayerShips(connection, name);

        }
    }
}
