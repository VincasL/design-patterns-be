using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Mines
{
    public class RemoteControlledMine: Mine
    {
        public RemoteControlledMine()
        {
            Type = MineType.RemoteControlled;
        }
        
        private RemoteControlledMine(int damage, MineType type, int armourStrength, int explosionRadius, bool hasExploded)
        {
        }


        public override Unit Clone()
        {
            return new RemoteControlledMine(Damage, Type, ArmourStrength, ExplosionRadius, HasExploded);
        }
    }
}
