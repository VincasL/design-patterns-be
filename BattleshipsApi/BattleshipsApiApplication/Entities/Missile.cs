namespace BattleshipsApi.Entities
{
    public abstract class Missile:Unit
    {
        public int Fuel { get; set; }
        public int Dammage { get; set; }
    }
}
