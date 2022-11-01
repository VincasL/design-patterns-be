using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Mines
{
    public class RemoteControlledMIne: Mine
    {
        public RemoteControlledMIne()
        {
            Type = MineType.RemoteControlled;
        }
    }
}
