namespace BattleshipsApi.Models
{
    public class Game_Ship
    {
        public int Id { get; set; }

        public int ShipId { get; set; }

        public int GameId { get; set; }

        public int TileId { get; set; }

        public int PlayerId { get; set; }

    }
}
