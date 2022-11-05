using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Mines
{
    public class RemoteControlledMIne: Mine
    {
        public RemoteControlledMIne()
        {
            Type = MineType.RemoteControlled;
        }
        
        private RemoteControlledMIne(int dammage, MineType type, int armourStrength, int explosionRadious, bool hasExploded)
        {
        }


        public override Unit Clone()
        {
            return new RemoteControlledMIne(Dammage, Type, ArmourStrength, ExplosionRadious, HasExploded);
        }
    }
}
