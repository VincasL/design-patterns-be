using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Mines
{
    public class HugeMine : Mine
    {
        public HugeMine()
        {
            Type = MineType.Huge;
        }

        private HugeMine(int dammage, MineType type, int armourStrength, int explosionRadious, bool hasExploded)
        {
        }


        public override Unit Clone()
        {
            return new HugeMine(Damage, Type, ArmourStrength, ExplosionRadius, HasExploded);
        }
    }
}
