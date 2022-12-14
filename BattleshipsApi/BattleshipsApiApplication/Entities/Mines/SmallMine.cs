using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Mines
{
    public class SmallMine:Mine
    {
        public SmallMine()
        {
            Type = MineType.Small;
        }
        
        private SmallMine(int dammage, MineType type, int armourStrength, int explosionRadious, bool hasExploded)
        {
        }


        public override Unit Clone()
        {
            return new SmallMine(Damage, Type, ArmourStrength, ExplosionRadius, HasExploded);
        }
    }
}
