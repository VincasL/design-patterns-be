using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities
{
    public class Mine:Unit
    {
        public int ExplosionRadious { get; set; }
        public int Dammage { get; set; }
        public int ArmourStrength { get; set; }
        public MineType Type { get; set; }
    }
}
