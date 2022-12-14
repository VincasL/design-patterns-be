namespace BattleshipsApi.Entities
{
    public abstract class Projectile : Unit
    {
        public int ExplosionRadius { get; set; }
        public int Damage { get; set; }
        public int Fuel { get; set; }
    }
}
