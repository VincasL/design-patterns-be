namespace BattleshipsApi.Entities
{
    public abstract class Projectile:Unit
    {
        public int ExplosionRadious { get; set; }
        public int Dammage { get; set; }
        public int Fuel { get; set; }
    }
}
