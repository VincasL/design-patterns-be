using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities
{
    public abstract class Mine:Unit
    {
        public int ExplosionRadious { get; set; }
        public int Dammage { get; set; }
        public int ArmourStrength { get; set; }
        public MineType Type { get; set; }
        public bool HasExploded { get; set; }


        protected Mine(int explosionRadious, int dammage, int armourStrength, MineType type, bool hasExploded)
        {
            ExplosionRadious = explosionRadious;
            Dammage = dammage;
            ArmourStrength = armourStrength;
            Type = type;
            HasExploded = hasExploded;
        }

        protected Mine()
        {
        }
    }
}
